<script setup lang="ts">
import { Popover, PopoverButton, PopoverPanel } from '@headlessui/vue'
import {htmlResult, htmlSource, ResultMode} from '~/constants'

const resultMode = useState<ResultMode>('resultMode', () => ResultMode.Desktop)
const horizontalPanes = useState<boolean>('horizontalPanes')
const source = useState<string>('htmlSource', () => htmlSource)
const result = useState<string>('htmlResult', () => htmlResult)
const textResult = useState<string>('textResult', () => '')
const email = ref('')

async function renderHtml() {
  result.value = "";
  var response = await $fetch<any>('/render', {method: 'POST', body: {source: source.value}})
  result.value = response.html
  textResult.value = response.text
}
async function send(close: Function) {
  // result.value = "";
  var response = await $fetch<any>('/send', {method: 'POST', body: {source: source.value, email: email.value}})
  result.value = response.html
  textResult.value = response.text
  close()
}
</script>

<template>
  <nav class="bg-charade-950">
    <div class="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
      <div class="flex h-16 justify-between">
        <div class="flex">
          <div class="-ml-2 mr-2 flex items-center md:hidden"></div>
          <div class="flex flex-shrink-0 items-center gap-x-3 text-lg font-bold text-charade-200">
            <IconLogo class="h-8 w-auto"></IconLogo>
            <div>UnDotNet.BootstrapEmail</div>
          </div>
          <div class="ml-4 flex flex-shrink-0 items-center">
            <button
              type="button"
              class="relative inline-flex items-center gap-x-1.5 rounded-md bg-indigo-600 px-1 py-2 text-sm font-semibold text-white shadow-sm hover:bg-indigo-400 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-500 md:px-3"
              @click="renderHtml"
            >
              <IconRender class="-ml-0.5 h-5 w-5" aria-hidden="true" />
              Render
            </button>
          </div>
        </div>
        <div class="flex items-center">
          <div class="ml-4 flex flex-shrink-0 items-center gap-x-5">
            <div class="hidden gap-x-0 rounded bg-charade-900 px-1 py-1 text-sm sm:flex md:gap-x-2">
              <button
                type="button"
                :class="{ 'bg-charade-700': resultMode == ResultMode.Html }"
                class="relative rounded p-1 px-3 text-charade-200 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
                @click="resultMode = ResultMode.Html">
                <span class="absolute -inset-1.5" />
                <span class="hidden md:inline">HTML</span>
                <span class="md:hidden">H</span>
              </button>
              <button
                type="button"
                :class="{ 'bg-charade-700': resultMode == ResultMode.Text }"
                class="relative rounded p-1 px-3 text-charade-200 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
                @click="resultMode = ResultMode.Text">
                <span class="absolute -inset-1.5" />
                <span class="hidden md:inline">Text</span>
                <span class="md:hidden">T</span>
              </button>
              <button
                type="button"
                :class="{ 'bg-charade-700': resultMode == ResultMode.Desktop }"
                class="relative rounded p-1 px-3 text-charade-200 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
                @click="resultMode = ResultMode.Desktop">
                <span class="absolute -inset-1.5" />
                <span class="hidden md:inline">Desktop</span>
                <span class="md:hidden">D</span>
              </button>
              <button
                type="button"
                :class="{ 'bg-charade-700': resultMode == ResultMode.Mobile }"
                class="relative rounded p-1 px-3 text-charade-200 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800"
                @click="resultMode = ResultMode.Mobile">
                <span class="absolute -inset-1.5" />
                <span class="hidden md:inline">Mobile</span>
                <span class="md:hidden">M</span>
              </button>
            </div>

            <button
              @click="horizontalPanes = !horizontalPanes"
              type="button"
              class="relative hidden rounded bg-charade-700 p-1 px-2 text-charade-200 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800 lg:flex">
              <IconHorizontal v-if="!horizontalPanes" class="h-6 w-6 rounded" />
              <IconVertical v-if="horizontalPanes" class="h-6 w-6 rounded" />
              <span class="sr-only">Layout</span>
            </button>

            <Popover as="div" class="relative ml-3">
              <div>
                <PopoverButton
                  class="relative flex items-center gap-x-3 rounded bg-indigo-600 p-1 px-3 text-sm text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
                  <IconSend class="h-6 w-6 rounded" />
                  <span class="hidden lg:block">Send</span>
                  <IconDropdown class="h-4 w-4 rounded" />
                </PopoverButton>
              </div>
              <transition
                enter-active-class="transition ease-out duration-200"
                enter-from-class="transform opacity-0 scale-95"
                enter-to-class="transform opacity-100 scale-100"
                leave-active-class="transition ease-in duration-75"
                leave-from-class="transform opacity-100 scale-100"
                leave-to-class="transform opacity-0 scale-95">
                <PopoverPanel
                  class="absolute right-0 z-10 mt-2 flex w-64 origin-top-right flex-col rounded-md bg-white px-2 py-2 shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none"
                  v-slot="{ close }"
                  >
                  <label for="email" class="block text-sm font-medium leading-6 text-charade-700"
                    >Send test email to</label
                  >
                  <div class="flex gap-x-2">
                    <input
                      v-model="email"
                      type="email"
                      name="email"
                      id="email"
                      class="block w-full rounded-md border-0 px-1.5 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                      placeholder="you@example.com" />
                    <button
                      @click.prevent="send(close)"
                      type="button"
                      class="relative flex items-center gap-x-3 rounded bg-indigo-600 p-1 px-3 text-sm text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
                      Send
                    </button>
                  </div>
                </PopoverPanel>
              </transition>
            </Popover>
            <div class="hidden text-charade-200 lg:block">
              <a href="https://github.com/UnDotNet/BootstrapEmail" target="_blank"><IconGitHub class="h-9 w-auto"></IconGitHub></a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>
