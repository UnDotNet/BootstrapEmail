<script setup lang="ts">
useHead({
  title: 'UnDotNet.BootstrapEmail',
})
</script>

<template>
  <div class="flex h-screen w-screen flex-col bg-green-100">
    <div>
      <Header></Header>
    </div>
    <div class="flex grow flex-row overflow-hidden">
      <NuxtPage />
    </div>
    <div class="flex justify-between gap-x-8 bg-charade-950 px-5 py-2 text-sm text-charade-300">
      <div class="flex gap-x-2">
        <div>Made by <a href="https://github.com/JohnCampionJr" target="_blank" class="underline">John Campion</a></div>
        <div class="hidden sm:inline">
          | Ported from and credit to
          <a href="https://bootstrapemail.com" target="_blank" class="underline">bootstrapemail.com</a> - check out their amazing templates!
        </div>
      </div>
      <div>
        <div class="flex text-charade-300">
          <a href="https://github.com/UnDotNet/BootstrapEmail" target="_blank" class="flex gap-x-2">
            <IconGitHub class="h-5 w-auto"></IconGitHub>
            GitHub</a
          >
        </div>
      </div>
    </div>
  </div>
</template>

<style></style>
