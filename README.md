# 1. Na raiz do Desafio gerar Imagem Docker

docker build -t desafio -f docker/Dockerfile .

# 2. Subir docker compose do Banco de Dados e do APP

docker-compose -f docker/docker-compose.yml up -d

# 3. A API é publicada na porta 85
http://localhost:85/swagger/index.html

# 4. Para autenticar com o JWT chamar a api POST /authenticate 
{
  "document": "docadmin",
  "password": "admin"
}
- O usuário super admin padrão é criado no migration

# 5. O resultado do token pode ser utilizado para chamar as demais apis

Autenticação para obter o JWT: POST /authenticate

- Obtém a lista de funcionários: GET /list

- Obtém um funcionário pelo ID: GET /{id}

- Atualiza um funcionário pelo ID: PUT /{id}

- Deleta um funcionário pelo ID DELETE /{id}

- Altera a senha de um funcionário: PUT /{id}/change-password


# 6. Issues conhecidas
- Um funcionário pode trocar a senha de outro. Deve ser colocado uma validação para impedir que isso ocorra.
- Embora um funcionário não possa criar ou editar um funcionário de nivel superior, ele pode usar a api de edição para alterar o nível de acesso para um inferior e depois editá-lo. Deve ser colocada uma validação para barrar complementamente a edição de funcionários superiores. 
- A lista de funcionários e o método de obter funcionário retornam o hash da senha. Essa não é uma boa prática.
- É permitido trocar a senha de um funcionário usando o método de editar. Essa não é uma boa prática.
- Existe validação duplicidade do número do documento, mas não há uma constraint unique no banco, apenas validação no código. Deve ser colocada a constraint.
- Não há validação de telefone
- O Swagger está sem documentação.

 
