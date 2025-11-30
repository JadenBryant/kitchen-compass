FROM nginx:alpine
RUN rm -rf /etc/nginx/templates
COPY ./reverse-proxy/nginx.conf /etc/nginx/conf.d/default.conf
