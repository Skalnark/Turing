
# Turing Machines 

(Precismos de um nome pro software)

## TODO List | Parte 1 do Projeto
- ### TuringMachine.cs
	- **Implementar a função de leitura de arquivo contendo a descrição da máquina, a função deverá instanciar a máquina.**
	- **Implementar a função que grava máquina registrada em arquivo.**
		- Implementar a função de registro de máquina, que deverá também salvar a máquina em outro arquivo. 
			- Criada uma função para pegar o registro de uma máquina inserida e salvar no disco ✓
		- Criar a cena de registro de máquina.
	- **Implementar as funções de início e parada de processamento (estudar uso de thread em Unity5).**
- ### State.cs
	- **Alterar a função que processa a profundidade da recursividade da função State.process() para que ela exiba suas mensagens no quadro de aviso**
		- A função State.process() continua precisando da implementação das chamadas de função para fazer brilhar os botões de aceitação e rejeição. 
- ### MachineGear.cs
	- **Fazer com que o movimento de OnInputMove() seja incrementado gradualmente, para criar um movimento mais fluído.**
	- **Concluir a implementação da função OnSymbolInsert(), fazendo que o objeto da fita infinita aumente de acordo com o número de símbolos inseridos.**
- ### Utils.cs
	- **Essa função deve conter todas as funções genéricas necessárias, pra garantir a legibilidade e o reaproveitamento de código.**
		- Todas as funções estáticas deverão estar aqui.
- ### CameraControl.cs
	- **Precisamos limitar para onde a máquina e a luz que acompanha a máquina podem ir baseado no tamanho da fita.**
- ### Funcionalidades faltando
	- **Precisamos criar uma lista encadeada que una as células da fita de entrada, pra que saibamos qual o primeiro símbolo, qual o último e não tenhamos problema em remover símbolos.**
	- **Para a inserção dos valores de cada célula, precisamos criar dois tipos de inserção:**
		- O primeiro tipo deverá ser simplesmente uma caixa de texto onde o usuário diz o input, para agilizar o processo de inserção.
		- O segundo tipo deverá ser um tipo de "botão" de cada célula, que permita o usuário clicar na célula e inserir um valor pra ela.
	- **Precisamos criar os botões de ligar e desligar a máquina.**
		-  Sem a desculpa esfarrapada de *"Ah mas eu não sei usar o blender"* por que eu também não sei e fiz mesmo assim. Youtube tá aí pra isso).
	- **Precisamos criar uma variável global que determine a velocidade de processamento.**
		-  O usuário pode querer apenas ver o resultado do processamento, ou pode querer ver a máquina trabalhando.