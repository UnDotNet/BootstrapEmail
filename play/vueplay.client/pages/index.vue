<!--suppress CssUnusedSymbol, SpellCheckingInspection -->
<script setup lang="ts">
import { Splitpanes, Pane } from 'splitpanes'
import 'splitpanes/dist/splitpanes.css'

const horizontalPanes = useState<boolean>('horizontalPanes')
const isResizing = ref(false)
const showResolution = ref(false)

watch(isResizing, () => {
  if (isResizing.value != showResolution.value) {
    if (!showResolution.value) {
      showResolution.value = true
    } else {
      setTimeout(() => {
        showResolution.value = false
      }, 1000)
    }
  }
})
</script>
<template>
  <splitpanes @resize="isResizing = true" @resized="isResizing = false" :horizontal="horizontalPanes">
    <pane>
      <PaneSource></PaneSource>
    </pane>
    <pane>
      <PaneResult :show-resolution="showResolution"></PaneResult>
    </pane>
  </splitpanes>
</template>

<style lang="postcss">
.splitpanes--vertical > .splitpanes__splitter,
.splitpanes--vertical > .splitpanes__splitter {
  width: 9px;
  margin: 0 -3px;
  border-left: 3px solid transparent;
  border-right: 3px solid transparent;
  cursor: ew-resize;
  @apply bg-charade-700;
}

.splitpanes--horizontal > .splitpanes__splitter,
.splitpanes--horizontal > .splitpanes__splitter {
  height: 9px;
  margin: -3px 0;
  border-top: 3px solid transparent;
  border-bottom: 3px solid transparent;
  cursor: ns-resize;
  @apply bg-charade-700;
}

.splitpanes__splitter {
  box-sizing: border-box;
  position: relative;
  flex-shrink: 0;
}

.cm-editor .cm-content {
  font-family: 'SF Mono', Monaco, Menlo, Consolas, 'Ubuntu Mono', 'Liberation Mono', 'DejaVu Sans Mono', 'Courier New',
    monospace;
  font-size: 13px;
}

.cm-editor {
  height: 100%;
  width: 100%;
}
</style>
