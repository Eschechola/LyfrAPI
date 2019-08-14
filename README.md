# LyfrAPI

<h3><font color="red">Rotas</font></h3>

<p><strong>Vou alterar sábado pra so aceitar POST PUT e DELETE da nossa aplicação, por enquanto usa assim.</strong></p>


<strong><h1>CLIENTE</h1></strong>

<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
https://restlyfrapi.herokuapp.com/api/Cliente/Insert/?json=json&senhaAPI=Lyfr123

-------------------------------------------------------------------------------------------------

Alterar
HTTP - PUT
https://restlyfrapi.herokuapp.com/api/Cliente/Alter/?json=json&senhaAPI=Lyfr123


-------------------------------------------------------------------------------------------------

Ler
HTTP - GET
https://restlyfrapi.herokuapp.com/api/Cliente/GetAllClientes/?senhaAPI=Lyfr123

HTTP - GET
https://restlyfrapi.herokuapp.com/api/Cliente/GetClienteByEmail/?email=email&senha=senha&senhaAPI=Lyfr123


HTTP - GET
https://restlyfrapi.herokuapp.com/api/Cliente/GetClienteByCPF/?cpf=cpf&senha=senha&senhaAPI=Lyfr123

-------------------------------------------------------------------------------------------------

DELETAR
HTTP - DELETE
https://restlyfrapi.herokuapp.com/api/Cliente/DeleteByEmail/?email=email&senhaAPI=Lyfr123

HTTP - DELETE
https://restlyfrapi.herokuapp.com/api/Cliente/DeleteByCPF/?cpf=cpf&senhaAPI=Lyfr123

</pre>









<br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br><br>

<strong><h1>ADMINISTRADOR</h1></strong>


<pre>


-------------------------------------------------------------------------------------------------

Inserir
HTTP - Post
https://restlyfrapi.herokuapp.com/api/Administrador/Insert/?json=json&senhaAPI=Lyfr123

-------------------------------------------------------------------------------------------------

Alterar
HTTP - PUT
https://restlyfrapi.herokuapp.com/api/Administrador/Alter/?json=json&senhaAPI=Lyfr123


-------------------------------------------------------------------------------------------------

Ler
HTTP - GET
https://restlyfrapi.herokuapp.com/api/Administrador/GetAllAdministradores/?senhaAPI=Lyfr123

HTTP - GET
https://restlyfrapi.herokuapp.com/api/Administrador/GetAdministrador/?login=login&senha=senha&senhaAPI=Lyfr123


-------------------------------------------------------------------------------------------------

DELETAR
HTTP - DELETE
https://restlyfrapi.herokuapp.com/api/Administrador/DeleteByLogin/?login=login&senhaAPI=Lyfr123

</pre>
