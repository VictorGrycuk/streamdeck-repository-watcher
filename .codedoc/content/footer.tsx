import { CodedocConfig } from '@codedoc/core';
import { Footer as _Footer, GitterToggle$, Watermark} from '@codedoc/core/components';


export function Footer(config: CodedocConfig, renderer: any) {
  let bmc$ = <script type="text/javascript" src="/docs/assets/bmc.js" data-name="bmc-button" data-slug="victorgrycuk" data-color="#00000000" data-emoji="" data-font="Cookie" data-text="Buy me a coffee" data-outline-color="#545454" data-coffee-color="#000000" ></script>;
  let github$;
  if (config.misc?.github)
    github$ = <a href={`https://github.com/${config.misc.github.user}/${config.misc.github.repo}/`} 
                target="_blank">GitHub</a>;

  let community$;
  if (config.misc?.gitter)
    community$ = <GitterToggle$ room={config.misc.gitter.room}/>

  if (github$ && bmc$) return <_Footer>{github$}<hr/>{bmc$}</_Footer>;
  else if (github$) return <_Footer>{github$}</_Footer>;
  else if (community$) return <_Footer>{community$}</_Footer>;
  else return <_Footer><Watermark/></_Footer>;
}