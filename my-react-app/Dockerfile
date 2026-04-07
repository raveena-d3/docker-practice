# ─────────────────────────────────────────────
# STAGE 1: Build
# Uses Node.js to install dependencies and build
# ─────────────────────────────────────────────
FROM node:20-alpine AS build

# Set working directory
WORKDIR /app

# Copy package files first (better layer caching)
COPY package*.json ./
RUN npm install

# Copy all source files
COPY . .

# Build the Vite/React app for production
RUN npm run build

# ─────────────────────────────────────────────
# STAGE 2: Serve
# Uses Nginx to serve the static build output
# ─────────────────────────────────────────────
FROM nginx:alpine AS runtime

# This line copies the built React app from the build stage into Nginx’s public folder so it can be served to users.
COPY --from=build /app/dist /usr/share/nginx/html

# Copy your custom Nginx settings into the container so Nginx uses your rules instead of default ones.
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80

# Nginx starts automatically
CMD ["nginx", "-g", "daemon off;"]
#daemon -> the program which runs in the background 
# and daemon off means stops running in the background runs in the foreground 
# Foreground and background do not refer to frontend and backend. 
#Instead, they describe how a program runs. 
#In Docker, both frontend and backend must run in the foreground so that the container stays alive.
#Every container needs one foreground process to stay alive