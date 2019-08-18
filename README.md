# LyfrAPI

<h3><font color="red">Rotas</font></h3>

<br>

<h2>Explicação JWT (SEGURANÇA)</h2>

<br>

  <h5>
    A segurança via JSON WEB TOKENS funciona da seguinte forma, todos os métodos da API (ROTAS), tem um atributo chamado  [Authorize], esse atributo deixa o método inacessível para qualquer pessoa, necessitando ter uma key para acesso a API, (como se fosse uma senha), essa key é temporária e expira com o tempo, tendo que gerar ela novamente pra acessar novamente a API, então antes de realizar qualquer requisição para dados, voce tem que pedir a key pra API na rota:
    <br>
    <br><br>
    <strong>https://lyfrapi1.herokuapp.com/api/Seguranca/LoginAPI<br><br>*ÚNICO MÉTODO QUE NÃO NECESSITA DE KEY NA API*</strong><br><br>
    Enviando via POST um json nesse formato:<br><br>
    <pre>
      {
        "Nome":"Lyfr_User123",
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

HTTP - GET
https://lyfrapi1.herokuapp.com/api/Cliente/GetClienteByEmail/

Enviar o Email via GET

-------------------------------------------------------------------------------------------------

HTTP - GET
https://lyfrapi1.herokuapp.com/api/Cliente/GetClienteByCPF/

Enviar o CPF via GET

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

HTTP - GET
https://lyfrapi1.herokuapp.com/api/Administrador/GetAdministrador/

Enviar o JSON do Model Administrador via GET (Só vai conter login e senha, os outros campos deixe em branco)

-------------------------------------------------------------------------------------------------

DELETAR
HTTP - DELETE
https://lyfrapi1.herokuapp.com/api/Administrador/DeleteByLogin/

Enviar o Login via DELETE

-------------------------------------------------------------------------------------------------

</pre>
