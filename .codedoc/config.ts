import { configuration } from '@codedoc/core';

import { theme } from './theme';


export const config = /*#__PURE__*/configuration({
  theme,
  dest: {
    namespace: '/streamdeck-repository-watcher'
  },
  page: {
    title: {
      base: 'StreamDeck Repository Watcher'
    }
  },
  misc: {
    github: {
      user: 'VictorGrycuk',
      repo: 'streamdeck-repository-watcher',
      action: 'Follow',
    },
    bmc: {
      buttonPath: ""
    }
  }
});
