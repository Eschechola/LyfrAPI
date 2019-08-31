# LyfrAPI

<br><br><br>

<h2>Explicação JWT (SEGURANÇA)</h2>

<br>

  <h5>
    A segurança via JSON WEB TOKENS funciona da seguinte forma, todos os métodos da API (ROTAS), tem um atributo chamado  [Authorize], esse atributo deixa o método inacessível para qualquer pessoa, necessitando ter uma key para acesso a API, (como se fosse uma senha), essa key é temporária e expira com o tempo, tendo que gerar ela novamente pra acessar novamente a API, então antes de realizar qualquer requisição para dados, voce tem que pedir a key pra API na rota:
    <br>
    <br><br>
  <strong><pre>https://lyfrapi1.herokuapp.com/api/Seguranca/LoginAPI</pre><br>*ÚNICO MÉTODO QUE NÃO NECESSITA DE KEY NA API*</strong><br><br><br>
    Enviando via POST um json nesse formato:<br><br>
    <pre>
      {
        "Usuario":"Lyfr_User123",
        "Senha": "LyfrAPI123",
        "TipoUsuario": "M"
      }
    </pre>
    <br>
    Enviando esses dados ele irá te retornar uma key, caso os dados do json estejam errados ou inválidos vai retornar um HTTP 401 (Sem autorização), senão vai retornar um HTTP 200 (Sucesso) e a key de string, para verificar se a requisição foi bem sucedida use o método:
  <br><br><br>
  <pre>
    //exemplo de post
    var response = await client.PostAsync(uri, content);
     if (response.IsSuccessStatusCode)
     {
        //...
     }
  </pre>
  <br>
   Para enviar a key no HEADER do json utilize:
   <br><br><br>
   <pre>
   HttpClient client = new HttpClient();
   client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "KEY AQUI");
   </pre>
   
   <br>
   
   <strong>Com essa key você pode utilizar qualquer rota da API e ter acesso aos dados.</strong>
  <h5>
<br><br>


<strong><h1>CLIENTE</h1></strong>


<pre>

-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
https://lyfrapi1.herokuapp.com/api/Cliente/Insert/

Enviar o JSON do Model Cliente via POST

-------------------------------------------------------------------------------------------------

Alterar
HTTP - PUT
https://lyfrapi1.herokuapp.com/api/Cliente/Alter/

Enviar o JSON do Model Cliente via PUT (Com os dados ja alterados)

-------------------------------------------------------------------------------------------------

Ler
HTTP - GET
https://lyfrapi1.herokuapp.com/api/Cliente/GetAllClientes/

Não enviar nada

-------------------------------------------------------------------------------------------------

HTTP - Post
https://lyfrapi1.herokuapp.com/api/Cliente/GetClienteByEmail/

Enviar o ClienteLogin via Post

-------------------------------------------------------------------------------------------------

HTTP - Post
https://lyfrapi1.herokuapp.com/api/Cliente/GetClienteByCPF/

Enviar o ClienteLogin via Post

-------------------------------------------------------------------------------------------------

DELETAR
HTTP - DELETE
https://lyfrapi1.herokuapp.com/api/Cliente/DeleteByEmail/

Enviar o Email via DELETE

-------------------------------------------------------------------------------------------------

HTTP - DELETE
https://lyfrapi1.herokuapp.com/api/Cliente/DeleteByCPF/

Enviar o CPF via DELETE

-------------------------------------------------------------------------------------------------

</pre>



<br><br><br>

<strong><h1>ADMINISTRADOR</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
https://lyfrapi1.herokuapp.com/api/Administrador/Insert/

Enviar o JSON do Model Administrador via POST

-------------------------------------------------------------------------------------------------

Alterar
HTTP - PUT
https://lyfrapi1.herokuapp.com/api/Administrador/Alter/


Enviar o JSON do Model Administrador via PUT

-------------------------------------------------------------------------------------------------

Ler
HTTP - GET
https://lyfrapi1.herokuapp.com/api/Administrador/GetAllAdministradores/

Não enviar nada

-------------------------------------------------------------------------------------------------

HTTP - Post
https://lyfrapi1.herokuapp.com/api/Administrador/GetAdministrador/

Enviar o JSON do Model AdministradorLogin via Post

-------------------------------------------------------------------------------------------------

DELETAR
HTTP - DELETE
https://lyfrapi1.herokuapp.com/api/Administrador/DeleteByLogin/

Enviar o Login via DELETE

-------------------------------------------------------------------------------------------------

</pre>

<br><br><br>

<strong><h1>EDITORA</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
https://lyfrapi1.herokuapp.com/api/Editora/Insert/

Enviar o JSON do Model Editora via POST

-------------------------------------------------------------------------------------------------

Ler

HTTP - Get
https://lyfrapi1.herokuapp.com/api/Editora/GetAllEditoras

Não enviar nada

HTTP - POST
https://lyfrapi1.herokuapp.com/api/Editora/GetEditoraByNome

Enivar o nome da editora

-------------------------------------------------------------------------------------------------

Deletar

HTTP - POST
https://lyfrapi1.herokuapp.com/api/Editora/DeleteByNome

Enivar o nome da editora

-------------------------------------------------------------------------------------------------
</pre>

<br><br><br>

<strong><h1>SUGESTÃO</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
https://lyfrapi1.herokuapp.com/api/Sugestao/Insert/

Enviar o JSON do Model Sugestao via POST

-------------------------------------------------------------------------------------------------

Ler

HTTP - Get
https://lyfrapi1.herokuapp.com/api/Sugestao/GetAllSugestoes

Não enviar nada

HTTP - POST
https://lyfrapi1.herokuapp.com/api/Sugestao/GetSugestoesById

Enviar o id da sugestao

HTTP - POST
https://lyfrapi1.herokuapp.com/api/Sugestao/GetSugestoesByIdCliente

Enviar o id do cliente

-------------------------------------------------------------------------------------------------

</pre>

<br><br><br>

<strong><h1>GENERO</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
https://lyfrapi1.herokuapp.com/api/Genero/Insert/

Enviar o JSON do Model Genero via POST

-------------------------------------------------------------------------------------------------

Ler

HTTP - Get
https://lyfrapi1.herokuapp.com/api/Genero/GetAllGeneros

Não enviar nada

HTTP - POST
https://lyfrapi1.herokuapp.com/api/Genero/GetGeneroByNome

Enviar o nome do genero

-------------------------------------------------------------------------------------------------

Deletar

HTTP - Delete
https://lyfrapi1.herokuapp.com/api/Genero/DeleteByNome

Enviar o nome do genero

</pre>
