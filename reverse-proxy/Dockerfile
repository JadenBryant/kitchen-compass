FROM nginx:alpine
RUN rm -rf /etc/nginx/templates /docker-entrypoint.d/*
COPY ./reverse-proxy/nginx.conf /etc/nginx/conf.d/default.conf
