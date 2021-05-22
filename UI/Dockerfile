FROM node:alpine as builder
WORKDIR /app
COPY . /app/
RUN npm install
RUN npm run buildProd

FROM nginx:1.17.1-alpine
RUN rm -rf /usr/share/nginx/html/*
COPY justfind.ru.com.crt /etc/nginx/justfind.ru.com.crt
COPY justfind.ru.com.key /etc/nginx/justfind.ru.com.key
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /app/public/. /usr/share/nginx/html
