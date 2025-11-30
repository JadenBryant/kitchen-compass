#!/bin/sh
set -e

: "${API_ORIGIN:?API_ORIGIN env var is required}"
: "${FRONTEND_ORIGIN:?FRONTEND_ORIGIN env var is required}"

# Render the template using environment variables
envsubst '$API_ORIGIN $FRONTEND_ORIGIN' \
  < /etc/nginx/templates/default.conf.template \
  > /etc/nginx/conf.d/default.conf

exec nginx -g 'daemon off;'
