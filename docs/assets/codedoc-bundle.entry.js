import { getRenderer } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/transport/renderer.js';
import { initJssCs } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/transport/setup-jss.js';initJssCs();
import { installTheme } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/content/theme.ts';installTheme();
import { codeSelection } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/code/selection.js';codeSelection();
import { sameLineLengthInCodes } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/code/same-line-length.js';sameLineLengthInCodes();
import { initHintBox } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/code/line-hint/index.js';initHintBox();
import { initCodeLineRef } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/code/line-ref/index.js';initCodeLineRef();
import { initSmartCopy } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/code/smart-copy.js';initSmartCopy();
import { copyHeadings } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/heading/copy-headings.js';copyHeadings();
import { contentNavHighlight } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/page/contentnav/highlight.js';contentNavHighlight();
import { loadDeferredIFrames } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/transport/deferred-iframe.js';loadDeferredIFrames();
import { smoothLoading } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/transport/smooth-loading.js';smoothLoading();
import { tocHighlight } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/page/toc/toc-highlight.js';tocHighlight();
import { postNavSearch } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/page/toc/search/post-nav/index.js';postNavSearch();
import { copyLineLinks } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/code/line-links/copy-line-link.js';copyLineLinks();
import { gatherFootnotes } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/footnote/gather-footnotes.js';gatherFootnotes();
import { ToCPrevNext } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/page/toc/prevnext/index.js';
import { CollapseControl } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/collapse/collapse-control.js';
import { GithubSearch } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/misc/github/search.js';
import { ToCToggle } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/page/toc/toggle/index.js';
import { DarkModeSwitch } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/components/darkmode/index.js';
import { ConfigTransport } from 'C:/Repositories/streamdeck-repository-watcher/.codedoc/node_modules/@codedoc/core/dist/es5/transport/config.js';

const components = {
  'MpIzQdT+HYVbLbOKcRiu0Q==': ToCPrevNext,
  'OVSglutkgnTQly+5q2nX0Q==': CollapseControl,
  'Zgjl9qvaVE7qz57I0g9TMw==': GithubSearch,
  '83/mw7glVA9261Y4RPm4FA==': ToCToggle,
  'z/CwTsAaY6SXvQUBYphTMA==': DarkModeSwitch,
  '9t+g/w2fRqr4dKtTrZSIyA==': ConfigTransport
};

const renderer = getRenderer();
const ogtransport = window.__sdh_transport;
window.__sdh_transport = function(id, hash, props) {
  if (hash in components) {
    const target = document.getElementById(id);
    renderer.render(renderer.create(components[hash], props)).after(target);
    target.remove();
  }
  else if (ogtransport) ogtransport(id, hash, props);
}
