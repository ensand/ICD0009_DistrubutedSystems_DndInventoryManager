FROM node:10
 
# A directory within the virtualized Docker environment
WORKDIR /src
 
# Copies package.json and package-lock.json to Docker environment
COPY package*.json ./ 
RUN npm install
 
# Copies everything over to Docker environment
COPY . .

#RUN npm run build

# Uses port which is used by the actual application
EXPOSE 3000
 
# Finally runs the application
CMD [ "npm", "start" ]