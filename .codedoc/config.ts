import { configuration } from '@codedoc/core';
import { googleAnalytics } from './ga-plugin';
import { theme } from './theme';


export const config = /*#__PURE__*/configuration({
  theme,
  dest: {
    namespace: '/streamdeck-repository-watcher'
  },
  page: {
    title: {
      base: 'StreamDeck Repository Watcher'
    },
    favicon: "favicon.ico"
  },
  misc: {
    github: {
      user: 'VictorGrycuk',
      repo: 'streamdeck-repository-watcher',
      action: 'Follow',
    }
  },
  plugins: [
    googleAnalytics('UA-60026499-1')
  ],
});
