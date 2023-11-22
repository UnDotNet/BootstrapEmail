<script setup lang="ts">
import CodeMirror from 'vue-codemirror6'
import { html } from '@codemirror/lang-html'
import { dracula } from 'thememirror'
import { htmlSource } from '~/constants'
import { useElementHover, useClipboard } from '@vueuse/core'

const extensions = [html(), dracula]
const source = useState<string>('htmlSource', () => htmlSource)

const editor = ref()
const sourceCopy = ref()
const editorHovered = useElementHover(editor)
const copyButtonHovered = useElementHover(sourceCopy)
const { copy, copied } = useClipboard()
</script>

<template>
  <div class="relative h-full w-full overflow-auto">
    <code-mirror
      ref="editor"
      v-model="source"
      :basic="true"
      placeholder="HTML goes here..."
      :extensions="extensions"
      :style="{ height: '100%', width: '100%' }"
      :tab="true"
      :tab-size="2"></code-mirror>
  </div>
  <div class="relative">
    <button
      type="button"
      ref="sourceCopy"
      @click="copy(<string>source)"
      :class="{ 'opacity-100 focus:pointer-events-auto focus:opacity-100': editorHovered || copyButtonHovered }"
      class="absolute bottom-4 right-[calc(14px+0.625rem)] flex select-none items-center rounded bg-charade-50 py-0.5 pl-2 pr-2.5 text-xs font-semibold leading-6 text-charade-200 opacity-0 transition-opacity duration-500 hover:bg-charade-100 dark:bg-charade-700 dark:text-charade-400 dark:hover:bg-charade-600">
      <IconCopy class="mr-1 h-5 w-5 flex-none text-charade-400" />
      <span v-if="!copied">Copy<span class="sr-only">, then focus editor</span></span>
      <span v-else>Copied!</span>
    </button>
  </div>
</template>
