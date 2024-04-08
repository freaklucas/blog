## Padronizando ações da API

As respostas de sucesso e insucesso são padronizadas com base na classe ResultViewModel

[x] Caso de sucesso:

Put to https://localhost:44335/v1/categories/6
````
{
	"name": "FullStack",
	"slug": "fullstack 2"
}

````

Response 200

```` 
    {
	"data": {
		"id": 6,
		"name": "FullStack",
		"slug": "fullstack 2",
		"posts": null
	},
	"errors": []
}
````

[x] Caso de insucesso: (id não existente)

Put to https://localhost:44335/v1/categories/1
````
{
	"name": "FullStack",
	"slug": "fullstack 2"
}

````

Response 404

```` 
{
	"data": null,
	"errors": [
		"Identificador não encontrado."
	]
}
````