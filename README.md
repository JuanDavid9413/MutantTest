# MutantTest

1. La logica del proceso consiste en sacar las dimensiones de los registros para crear una matriz y la matriz se recorre horizontal, vertical y diagonal 
para encontrar los patrones del DNA.
2. El programa fue creado a N capas.
3. Esta desarrollado en C# con .NetCore 3.1
4. Tiene una covertura de pruebas unitarias de 99,9%.
5. La base de datos se esta construida en SQL Server.
5. La aplicacion se encuentra desplegada en Azure.
7. Las URL son las siguientes:
	* Metodo POST:  https://mutanttest.azurewebsites.net/api/Mutants/mutant
		JSON:
		{
			"dna":["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
		}
	* Metodo GET:   https://mutanttest.azurewebsites.net/api/Mutants/stats
