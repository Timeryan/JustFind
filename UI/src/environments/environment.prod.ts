import { DadataType } from '@kolkov/ngx-dadata';

export const environment = {
  production: true,
  apiUrl: 'https://justfind.ru.com:5000',
  applicationUrl: 'https://justfind.ru.com',
  vkOAuthTokenUrl: 'https://oauth.vk.com/access_token',
  vkOAuthLink: 'https://oauth.vk.com/authorize',
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
