# App de Tarefa #
# NET 8, Visual Studio e VSCode #
# Woindows #
## App de tarefa em .NET 8 com RabbitMq, SQL-Server e Docker ##
#### Primeiro passo realizar o clone do repositório TarefasWeb ####
#### Segundo passo clonar os repositórios do worker e front ####
##### Repositório do worker: https://github.com/miguelcelso/worker #####
##### Repositório do front: https://github.com/miguelcelso/fronttarefa #####
#### Terceiro passo abrir o terminal (fica sua escolha), entrar na pasta "docker compose" do projeto TarefasWeb e executar o comando "docker-compose up" ####
##### Vai ser criado container do RabbitMq e SQL-Server no docker (Docker desktop) #####
#### Quarto passo abrir o projeto TarefasWeb no Visual Studio 2022 (Atualizado para trabalhar com .NET 8) ####
##### Criar a base de dados com Migration #####
###### Abra o Console do Gerenciador de Pacote que se encontra no menu Ferramentas/Gerenciador de PacoteS do NuGET ######
###### Execute os comandos ######
###### "Add-Migration iniciartarefa -project Infra" ######
###### "update-database -project Infra" ######
###### Caso de erro que o migration já existe, executar o comando "remove-migration" e depois repita os comandos acima ######
#### Quinto passo abrir o projeto worker no Visual Studio 2022 (Atualizado para trabalhar com .NET 8) ####
#### Sexto passo abrir o projeto fronttarefa no VSCode ####
##### Exceutar no terminal do VSCode o comando "npm install" para instalar os módulos do projeto ####
###### Instalar a versão mais recente do node.js e angular ######
###### https://nodejs.org/en/download/prebuilt-installer ######
###### executar no terminal "npm install -g @angular/cli" ######
#### Oitavo passo executar o projeto TarefasWeb ####
#### Nono passo executar o projeto fronttarefa no terminal o comando(npm start) ####
###### preencher a descrição no formulário e acionar o botão de cadastrar ######
#### Décimo passo executar o projeto worker ####
#### Décimo primeiro passo no front executar o botão de consultar para ver o resultado da leitura da fila no rabbitmq e a inclusão da tarefa no banco de dados ####


