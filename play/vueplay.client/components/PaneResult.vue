<script setup lang="ts">
import CodeMirror from 'vue-codemirror6'
import { html } from '@codemirror/lang-html'
import { dracula } from 'thememirror'
import { htmlResult, ResultMode } from '~/constants'
import { useElementHover, useClipboard, useElementSize } from '@vueuse/core'

defineProps<{
  showResolution?: boolean
}>()
const rightPane = ref()
const extensions = [html(), dracula]
const result = useState<string>('htmlResult', () => htmlResult)
const textResult = useState<string>('textResult', () => '')
const resultMode = useState<ResultMode>('resultMode', () => ResultMode.Desktop)
const { width, height } = useElementSize(rightPane)
const editor = ref()
const copyButton = ref()
const editorHovered = useElementHover(editor)
const copyButtonHovered = useElementHover(copyButton)
const { copy, copied } = useClipboard()
</script>

<template>
  <div class="flex h-full w-full justify-center bg-charade-950">
    <div
      v-if="resultMode != ResultMode.Html && resultMode != ResultMode.Text"
      ref="rightPane"
      :class="{ 'max-w-xs': resultMode == ResultMode.Mobile }"
      class="relative h-full w-full overflow-auto">
      <iframe
        :srcdoc="result"
        class="inset-0 h-full w-full bg-white"
        sandbox="allow-popups-to-escape-sandbox allow-scripts allow-popups allow-forms allow-pointer-lock allow-top-navigation allow-modals">
      </iframe>
      <div
        :class="{ 'opacity-100': showResolution }"
        class="absolute right-4 top-4 flex h-6 items-center rounded-full border bg-gray-700 px-3 text-xs leading-4 text-white opacity-0 shadow transition-opacity ease-out">
        {{ Math.round(width) }}x{{ Math.round(height) }}
      </div>
    </div>
    <div v-if="resultMode == ResultMode.Html" class="relative h-full w-full overflow-auto">
      <code-mirror
        ref="editor"
        :basic="true"
        v-model="result"
        placeholder="Click Render to generate the resulting HTML..."
        :extensions="extensions"
        :style="{ height: '100%', width: '100%' }"
        :tab="true"
        :tab-size="2"></code-mirror>
    </div>
    <div v-if="resultMode == ResultMode.Text" class="relative h-full w-full overflow-auto">
      <code-mirror
        ref="editor"
        :basic="true"
        v-model="textResult"
        placeholder="Click Render to generate the resulting text..."
        :extensions="extensions"
        :style="{ height: '100%', width: '100%' }"
        :tab="true"
        :tab-size="2"></code-mirror>
    </div>
  </div>
  <div class="relative">
    <button
      type="button"
      ref="copyButton"
      @click="copy(<string>result)"
      :class="{ 'opacity-100 focus:pointer-events-auto focus:opacity-100': editorHovered || copyButtonHovered }"
      class="absolute bottom-4 right-[calc(14px+0.625rem)] flex select-none items-center rounded bg-charade-50 py-0.5 pl-2 pr-2.5 text-xs font-semibold leading-6 text-charade-200 opacity-0 transition-opacity duration-500 hover:bg-charade-100 dark:bg-charade-700 dark:text-charade-400 dark:hover:bg-charade-600">
      <IconCopy class="mr-1 h-5 w-5 flex-none text-charade-400" />
      <span v-if="!copied">Copy<span class="sr-only">, then focus editor</span></span>
      <span v-else>Copied!</span>
    </button>
  </div>
</template>
