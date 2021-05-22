// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

import { DadataType } from '@kolkov/ngx-dadata';

export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000',
  applicationUrl: 'http://localhost',
  vkOAuthTokenUrl: 'http://oauth.vk.com/access_token',
  vkOAuthLink: 'http://oauth.vk.com/authorize',
  fbAppId: '925435254900273',
  googleAppId:
    '396617777786-r197g997el6fhsdjks8plbb07bqe7f8h.apps.googleusercontent.com',
  googleAppSecret: 'P00vw2pUhd8qAtRgyUrKjkgc',
  vkAppId: '7841674',
  vkOAuthClientSecret: 'Obwz3dOJY8ZkGjs987Uf',
  dadataConfig: {
    apiKey: 'f2a6c69e03cdb3c9b4590fdc5d0cbb36d26d9914',
    type: DadataType.address,
  },
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
