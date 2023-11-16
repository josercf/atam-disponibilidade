# Especifique a imagem base
FROM node:14

# Crie o diretório da aplicação
WORKDIR /usr/src/app

# Copie os arquivos package.json e package-lock.json para o diretório atual da imagem Docker
# A * é usada para se certificar de que tanto package.json quanto package-lock.json sejam copiados
# onde está disponível (npm@5+)
COPY package*.json ./

# Instale todas as dependências
RUN npm install

# Copie o restante dos arquivos da aplicação para o diretório atual da imagem Docker
COPY . .

# Exponha a porta 8080 para conexão (você pode mudar para a porta desejada)
EXPOSE 3000

# Defina o comando para executar a aplicação
CMD [ "node", "index.js" ]