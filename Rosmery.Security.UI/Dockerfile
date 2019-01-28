FROM nginx:latest
RUN rm /etc/nginx/nginx.conf
COPY nginx.conf /etc/nginx/
RUN rm /etc/nginx/conf.d/default.conf
COPY dist/rosmery-security-ui /usr/share/nginx/html
EXPOSE 80
EXPOSE 443