import child_process from 'node:child_process'
import fs from 'node:fs'
import path from 'node:path'
import process from 'node:process'

const baseFolder =
  process.env.APPDATA !== undefined && process.env.APPDATA !== ''
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`

const certificateArg = process.argv
  .map((arg) => arg.match(/--name=(?<value>.+)/i))
  .filter(Boolean)[0]
const certificateName = certificateArg
  ? certificateArg.groups!.value
  : 'vueplay'

if (!certificateName) {
  console.error(
    'Invalid certificate name. Run this script in the context of an npm/yarn script or pass --name=<<app>> explicitly.',
  )
  process.exit(-1)
}

const certFilePath = path.join(baseFolder, `${certificateName}.pem`)
const keyFilePath = path.join(baseFolder, `${certificateName}.key`)

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
  if (
    child_process.spawnSync(
      'dotnet',
      [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
      ],
      { stdio: 'inherit' },
    ).status !== 0
  ) {
    throw new Error('Could not create certificate.')
  }
}

export default defineNuxtConfig({
  modules: [
    '@vueuse/nuxt',
    '@pinia/nuxt',
    '@nuxtjs/color-mode',
    '@nuxtjs/tailwindcss',
    'nuxt-icon-tw',
  ],
  ssr: false,
  devServer: {
    https: {
      cert: certFilePath,
      key: keyFilePath,
    },
    port: 5002,
  },
  experimental: {
    // when using generate, payload js assets included in sw precache manifest
    // but missing on offline, disabling extraction it until fixed
    // payloadExtraction: false,
    // inlineSSRStyles: false,
    // renderJsonPayloads: true,
    typedPages: true,
  },

  colorMode: {
    classSuffix: '',
  },

  nitro: {
    esbuild: {
      options: {
        target: 'esnext',
      },
    },
    prerender: {
      crawlLinks: false,
      routes: ['/'],
      ignore: ['/hi'],
    },
  },

  app: {
    head: {
      viewport: 'width=device-width,initial-scale=1',
      link: [
        { rel: 'icon', href: '/favicon.ico', sizes: 'any' },
        {
          rel: 'icon',
          type: 'image/svg+xml',
          href: '/bootstrapemail_logo.svg',
        },
      ],
      meta: [
        { name: 'viewport', content: 'width=device-width, initial-scale=1' },
        { name: 'description', content: 'UnDotNet.BootstrapEmail Test Site' },
        {
          name: 'apple-mobile-web-app-status-bar-style',
          content: 'black-translucent',
        },
      ],
    },
  },

  vite: {
    server: {
      proxy: {
        '^/render': {
          target: 'https://localhost:5001/',
          secure: false,
        },
        '^/send': {
          target: 'https://localhost:5001/',
          secure: false,
        },
      },
    },
  },

  devtools: {
    enabled: true,
  },
  tailwindcss: {
    config: {
      // tailwind.config.js
      // https://uicolors.app/create
      // https://uicolors.app/edit?sv1=charade:50-f6f6f9/100-ecedf2/200-d5d7e2/300-afb3ca/400-848bac/500-656d92/600-505679/700-424662/800-393c53/900-2d2f3f/950-22232f
      theme: {
        extend: {
          colors: {
            charade: {
              '50': '#f6f6f9',
              '100': '#ecedf2',
              '200': '#d5d7e2',
              '300': '#afb3ca',
              '400': '#848bac',
              '500': '#656d92',
              '600': '#505679',
              '700': '#424662',
              '800': '#393c53',
              '900': '#2d2f3f',
              '950': '#22232f',
            },
          },
        },
      },
    },
  },
})
