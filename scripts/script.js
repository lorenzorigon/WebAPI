var tbody = document.querySelector('table tbody');
var aluno = {};


function Cadastrar() {
	aluno.nome = document.querySelector('#nome').value;
	aluno.sobrenome = document.querySelector('#sobrenome').value;
	aluno.telefone = document.querySelector('#telefone').value;
	aluno.ra = document.querySelector('#ra').value;



	if(aluno.id === undefined || aluno.id === 0)
	{
		salvarEstudantes('POST', 0 , aluno);
	}
	else 
	{
		salvarEstudantes('PUT', aluno.id , aluno);	
	}

	carregaEstudantes();
	Cancelar();

}

function novoAluno(){
	var btnSalvar = document.querySelector('#btnSalvar');
	var titulo = document.querySelector('#titulo');

	aluno = {};

	document.querySelector('#nome').value = '';
	document.querySelector('#sobrenome').value = '';
	document.querySelector('#ra').value = '';
	document.querySelector('#telefone').value = '';

	btnSalvar.textContent = 'Cadastrar';


	$('#exampleModal').modal('show');
}

function Cancelar(){
	var btnSalvar = document.querySelector('#btnSalvar');
	var titulo = document.querySelector('#titulo');

	aluno = {};

	document.querySelector('#nome').value = '';
	document.querySelector('#sobrenome').value = '';
	document.querySelector('#ra').value = '';
	document.querySelector('#telefone').value = '';

	btnSalvar.textContent = 'Cadastrar';


	$('#exampleModal').modal('hide');
}

function carregaEstudantes() {
	tbody.innerHTML = '';

	var xhr = new XMLHttpRequest();


	xhr.open(`GET`, `https://localhost:44389/Recuperar`, true);

	xhr.onreadystatechange = function() {
		if (this.readyState == 4 ){
			if(this.status == 200){ 
				var estudantes = JSON.parse(this.responseText);
				for(var indice in estudantes){
					adicionaLinha(estudantes[indice]);
				}
			}
			else if(this.status == 500){
				var erro = JSON.parse(this.responseText);
				console.log(erro);
			}
		}
	}


	xhr.send();

}

function salvarEstudantes(metodo, id, corpo) {

	var xhr = new XMLHttpRequest();

	if(id === undefined || id === 0)
		id = '';

	xhr.open(metodo, `https://localhost:44389/api/Aluno/${id}`, false);
	xhr.setRequestHeader('content-type', 'application/json');
	xhr.send(JSON.stringify(corpo));
}

function excluirEstudante(id){
	var xhr = new XMLHttpRequest();

	xhr.open('DELETE', `https://localhost:44389/api/Aluno/${id}`, false);
	xhr.send();
}

function excluir(id){
	bootbox.confirm({
		message: "Deseja realmente excluir?",
		buttons: {
			confirm: {
				label: 'Sim',
				className: 'btn-success'
			},
			cancel: {
				label: 'Não',
				className: 'btn-danger'
			}
		},
		callback: function (result) {
			if(result)
			{
				excluirEstudante(id);
				carregaEstudantes();
			}
		}
	});

}

carregaEstudantes();

function editarEstudante(estudante){
	var btnSalvar = document.querySelector('#btnSalvar');
	var titulo = document.querySelector('#titulo');
	document.querySelector('#nome').value = estudante.nome;
	document.querySelector('#sobrenome').value = estudante.sobrenome;
	document.querySelector('#ra').value = estudante.ra;
	document.querySelector('#telefone').value = estudante.telefone;

	aluno = estudante;

	btnSalvar.textContent = 'Salvar';
	titulo.textContent = `Editar Aluno ${estudante.nome}`;
}



function adicionaLinha(estudante) {

	var trow = `<tr>
	<td>${estudante.nome}</td>
	<td>${estudante.sobrenome}</td>
	<td>${estudante.ra}</td>
	<td>${estudante.telefone}</td>
	<td><button  class='btn btn-info
	' data-toggle="modal" data-target="#exampleModal" onclick='editarEstudante(${JSON.stringify(estudante)})'>Editar</button>
	<button class='btn btn-danger' onclick='excluir(${estudante.id})'>Excluir</button></td>
	</tr>
	`
	tbody.innerHTML += trow;
}