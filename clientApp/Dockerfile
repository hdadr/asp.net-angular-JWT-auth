FROM node:14.15.0-alpine3.11 as build
RUN mkdir -p /app
WORKDIR /app

COPY ./package.json .
RUN npm install
COPY . .
RUN npm run build --prod

FROM nginx:1-alpine as runtime
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /app/dist/clientApp /usr/share/nginx/html
COPY --from=build /app/default.conf /etc/nginx/conf.d/default.conf
