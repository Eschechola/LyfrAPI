# LyfrAPI

<br><br><br>

<h2>Explicação JWT (SEGURANÇA)</h2>

<br>

  <h5>
    A segurança via JSON WEB TOKENS funciona da seguinte forma, todos os métodos da API (ROTAS), tem um atributo chamado  [Authorize], esse atributo deixa o método inacessível para qualquer pessoa, necessitando ter uma key para acesso a API, (como se fosse uma senha), essa key é temporária e expira com o tempo, tendo que gerar ela novamente pra acessar novamente a API, então antes de realizar qualquer requisição para dados, voce tem que pedir a key pra API na rota:
    <br>
    <br><br>
  <strong><pre>http://www.lyfrapi.com.br/api/Seguranca/Login</pre><br>*ÚNICO MÉTODO QUE NÃO NECESSITA DE KEY NA API*</strong><br><br><br>
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
http://www.lyfrapi.com.br/api/Cliente/Insert/

Enviar o JSON do Model Cliente via POST

-------------------------------------------------------------------------------------------------

Alterar
HTTP - PUT
http://www.lyfrapi.com.br/api/Cliente/Update/

Enviar o JSON do Model Cliente via PUT (Com os dados ja alterados)

-------------------------------------------------------------------------------------------------

Ler
HTTP - GET
http://www.lyfrapi.com.br/api/Cliente/GetAllClientes/

Não enviar nada

-------------------------------------------------------------------------------------------------

HTTP - Post
http://www.lyfrapi.com.br/api/Cliente/GetClienteByEmail/

Enviar o ClienteLogin via Post

-------------------------------------------------------------------------------------------------

HTTP - Post
http://www.lyfrapi.com.br/api/Cliente/GetClienteByCPF/

Enviar o ClienteLogin via Post

-------------------------------------------------------------------------------------------------

DELETAR
HTTP - DELETE
http://www.lyfrapi.com.br/api/Cliente/DeleteByEmail/

Enviar o Email via DELETE

-------------------------------------------------------------------------------------------------

HTTP - DELETE
http://www.lyfrapi.com.br/api/Cliente/DeleteByCPF/

Enviar o CPF via DELETE

-------------------------------------------------------------------------------------------------

Esqueceu Senha
HTTP - POST
http://www.lyfrapi.com.br/api/Cliente/ForgotPassword/

Enviar o model RecoveryPassword via POST

-------------------------------------------------------------------------------------------------

</pre>



<br><br><br>

<strong><h1>ADMINISTRADOR</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
http://www.lyfrapi.com.br/api/Administrador/Insert/

Enviar o JSON do Model Administrador via POST

-------------------------------------------------------------------------------------------------

Alterar
HTTP - PUT
http://www.lyfrapi.com.br/api/Administrador/Update/


Enviar o JSON do Model Administrador via PUT

-------------------------------------------------------------------------------------------------

Ler
HTTP - GET
http://www.lyfrapi.com.br/api/Administrador/GetAllAdministradores/

Não enviar nada

-------------------------------------------------------------------------------------------------

HTTP - Post
http://www.lyfrapi.com.br/api/Administrador/GetAdministrador/

Enviar o JSON do Model AdministradorLogin via Post

-------------------------------------------------------------------------------------------------

DELETAR
HTTP - DELETE
http://www.lyfrapi.com.br/api/Administrador/DeleteByLogin/

Enviar o Login via DELETE

-------------------------------------------------------------------------------------------------

Esqueceu senha
HTTP - POST
http://www.lyfrapi.com.br/api/Administrador/ForgotPassword/

Enviar o model RecoveryPassword via POST

-------------------------------------------------------------------------------------------------

</pre>

<br><br><br>

<strong><h1>EDITORA</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
http://www.lyfrapi.com.br/api/Editora/Insert/

Enviar o JSON do Model Editora via POST

-------------------------------------------------------------------------------------------------

Ler

HTTP - Get
http://www.lyfrapi.com.br/api/Editora/GetAllEditoras

Não enviar nada

HTTP - POST
http://www.lyfrapi.com.br/api/Editora/GetEditoraByNome

Enivar o nome da editora

-------------------------------------------------------------------------------------------------

Deletar

HTTP - POST
http://www.lyfrapi.com.br/api/Editora/DeleteByNome

Enivar o nome da editora

-------------------------------------------------------------------------------------------------
</pre>

<br><br><br>

<strong><h1>SUGESTÃO</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
http://www.lyfrapi.com.br/api/Sugestao/Insert/

Enviar o JSON do Model Sugestao via POST

-------------------------------------------------------------------------------------------------

Ler

HTTP - Get
http://www.lyfrapi.com.br/api/Sugestao/GetAllSugestoes

Não enviar nada

HTTP - POST
http://www.lyfrapi.com.br/api/Sugestao/GetSugestaoById

Enviar o id da sugestao

HTTP - POST
http://www.lyfrapi.com.br/api/Sugestao/GetSugestoesByIdCliente

Enviar o id do cliente

-------------------------------------------------------------------------------------------------

RESPOSTA

HTTP - POST
http://www.lyfrapi.com.br/api/Sugestao/SugestaoResposta

Enviar o model SugestaoResposta via POST

-------------------------------------------------------------------------------------------------



</pre>

<br><br><br>

<strong><h1>GENERO</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
http://www.lyfrapi.com.br/api/Genero/Insert/

Enviar o JSON do Model Genero via POST

-------------------------------------------------------------------------------------------------

Ler

HTTP - Get
http://www.lyfrapi.com.br/api/Genero/GetAllGeneros

Não enviar nada

HTTP - POST
http://www.lyfrapi.com.br/api/Genero/GetGeneroByNome

Enviar o nome do genero

-------------------------------------------------------------------------------------------------

Deletar

HTTP - Delete
http://www.lyfrapi.com.br/api/Genero/DeleteByNome

Enviar o nome do genero

</pre>
 
<br><br><br>

<strong><h1>AUTORES</h1></strong>

<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
http://www.lyfrapi.com.br/api/Autor/Insert/

Enviar o JSON do Model Autor via POST

OBS: * NO CAMPO FOTO ENVIAR A FOTO EM BASE64. 
     * RESOLUÇÃO MÁXIMA 200x200 (caso seja maior, nao irá caber no tipo string, assim, a API irá negar).
     * ENVIAR TODAS AS FOTOS NO FORMATO JPG
-------------------------------------------------------------------------------------------------

Ler

HTTP - Get
http://www.lyfrapi.com.br/api/Autor/GetAllAutores

Não enviar nada

HTTP - POST
http://www.lyfrapi.com.br/api/Autor/GetAutorByNome

Enivar o nome da editora

-------------------------------------------------------------------------------------------------

Deletar

HTTP - POST
http://www.lyfrapi.com.br/api/Autor/DeleteByNome

Enivar o nome do autor

-------------------------------------------------------------------------------------------------
</pre>

