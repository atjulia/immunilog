# Etapa de Build
FROM node:20 AS build-stage

WORKDIR /app

COPY package.json ./
COPY package-lock.json ./

RUN npm install

COPY . .

RUN npm run build  # Cria os arquivos estáticos na pasta dist

# Etapa de Produção
FROM nginx:alpine AS production-stage

# Copia os arquivos do build para o diretório do Nginx
COPY --from=build-stage /app/dist /usr/share/nginx/html

# Copia o arquivo de configuração nginx.conf para o container
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
