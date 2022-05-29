/* REQUERIMIENTOS:
Con los conocimientos vistos hasta ahora en clase realizar un programa que haga lo siguiente:
Generar un programa que cree un cartón de bingo aleatorio y lo muestre por pantalla
1)    Cartón de 3 filas por 9 columnas
2)    El cartón debe tener 15 números y 12 espacios en blanco
3)    Cada fila debe tener 5 números
4)    Cada columna debe tener 1 o 2 números
5)    Ningún número puede repetirse
6)    La primer columna contiene los números del 1 al 9, la segunda del 10 al 19, la tercera del 20 al 29,
      así sucesivamente hasta la última columna la cual contiene del 80 al 90
7)    Mostrar el carton por pantalla

DISEÑO:     Generar un vector con cant de nros que iran en cada columna, iterar hasta conseguir 15 en total.
            Luego generar nros por columna(decena), guardar en vector aux, ordenar mayor y menor si son dos.
            Guardar en matriz cartonBingo, ubicarlos (1ra,2da,3re fila) aleatoriamente.
            Imprimir cartón
 */

// DESARROLLO
bool continuar = true;  // Mientras true

while (continuar)
{      // se siguen generando cartones

    // Leyenda inicial de presentación                   
    Console.WriteLine("┌──────────────────────────┐");
    Console.WriteLine("│    Generador de Bingo    │");
    Console.WriteLine("└──────────────────────────┘\n");
    int[,] cartonBingo = new int[3, 9]; // Matriz de 3 filas por 9 columnas Cartón de Bingo

    // Cuantos números por columna?
    Random parImpar = new Random();
    int[] vectorNrosXColumnas = new int[9]; // Guarda la cant de nros por columna. Ej vector(2,2,1,2,1,2,1,2,2)
    int suma = 0;
    do      // Itera hasta conseguir una combinación con 15 números
    {
        for (int i = 0; i < vectorNrosXColumnas.Length; i++)
        {
            vectorNrosXColumnas[i] = parImpar.Next(1, 3);   // Genera 2 ó 1 nro por columna
        }
        suma = 0;
        foreach (var item in vectorNrosXColumnas)
        {
            suma += item;       // Luego de generado el vector suma sus ítems
        }

    } while (suma != 15);       // Cuando sean 15 sale de bucle

    Random numeros = new Random();
    int posicionMatriz = 0;         // para posición en columna de matriz
    int decenaInicio = 1;           // para cálculo de las decenas
    int decenaFin = 10;             // para cálculo de las decenas
    int[] columnasAux = new int[3]; // guarda nros aleatorios de 1 columna

    // Lugares en columna
    Random random = new Random();
    int ubicacion = 0;              // Para colocar en 1ra,2da,3ra fila
    int cargarNro = 0;              // Aux para ordenar nros de mayor a menor
    int cargarNro2 = 0;             // Aux para ordenar nros de mayor a menor

    foreach (var item in vectorNrosXColumnas)
    {
        switch (item)
        {
            case 1:
                ubicacion = random.Next(1, 3);
                cargarNro = numeros.Next(decenaInicio, decenaFin);  // Genera nro según decena
                switch (ubicacion)              // Decide lugar entre 3 filas (para 1 solo nro en la columna)
                {
                    case 1:                     //nro,0,0
                        columnasAux[0] = cargarNro;
                        columnasAux[1] = 00;
                        columnasAux[2] = 00;
                        break;
                    case 2:                     //0,nro,0
                        columnasAux[0] = 00;
                        columnasAux[1] = cargarNro;
                        columnasAux[2] = 00;
                        break;
                    case 3:                     //0,0,nro
                        columnasAux[0] = 00;
                        columnasAux[1] = 00;
                        columnasAux[2] = cargarNro;
                        break;
                }
                break;

            case 2:   // Para 2 nros por columna
                ubicacion = random.Next(1, 3);
                bool nrosIguales = true;
                while (nrosIguales)     // Itera generando dos nros, hasta conseguir dos diferentes y ordenados menor y mayor                                    
                {
                    cargarNro = numeros.Next(decenaInicio, decenaFin);  // Genera 1er nro segun decena
                    cargarNro2 = numeros.Next(decenaInicio, decenaFin); // Genera 2do nro segun decena
                    if (cargarNro < cargarNro2)     // Solo entra si están ordenados menor-mayor...
                    {
                        nrosIguales = false;        // ...para salir del bucle
                    }
                }
                switch (ubicacion)// Decide lugar entre 2 lugares
                                  // y luego entre los 2 restante si el nro quedo en fila 0
                {
                    case 1:     // Ubicar 1er nro en fila 1 ó 2, ergo el 2do en fila 2 ó 3
                        columnasAux[0] = cargarNro; // nro,?,?
                        ubicacion = random.Next(1, 3);
                        //cargarNro = numeros.Next(decenaInicio, decenaFin);
                        if (ubicacion == 1)           // nro,nro,0
                        {
                            columnasAux[1] = cargarNro2;
                            columnasAux[2] = 00;
                        }
                        else if (ubicacion == 2)    // nro,0,nro
                        {
                            columnasAux[1] = 00;
                            columnasAux[2] = cargarNro2;
                        }
                        break;

                    case 2:
                        ubicacion = random.Next(1, 2);  // 0,nro,nro
                        //cargarNro = numeros.Next(decenaInicio, decenaFin);
                        columnasAux[0] = 0;
                        columnasAux[1] = cargarNro;
                        //cargarNro = numeros.Next(decenaInicio, decenaFin);
                        columnasAux[2] = cargarNro2;
                        break;
                }
                break;
        }
        for (int filas = 0; filas < 3; filas++)     // Va guardando en "el cartón" cada una de las columnas generadas
        {
            cartonBingo[filas, posicionMatriz] = columnasAux[filas];
        }
        posicionMatriz++;               // Avanza en las columnas
        decenaFin = decenaFin + 10;      // Prepara indicadores de decena
        decenaInicio = decenaFin - 9;
    }

    // Mostrar cartón
    Console.WriteLine("╔══╦══╦══╦══╦══╦══╦══╦══╦══╗");
    for (int fila = 0; fila < 3; fila++)
    {
        for (int columna = 0; columna < 9; columna++)
        {
            int valorNro = cartonBingo[fila, columna];
            if (valorNro < 10)                      // Imprime por filas
            {
                if (valorNro == 00)
                {
                    Console.Write("║░░");     // Si es nro 0(cero) imprime cuadro sombreado
                }
                else
                {
                    Console.Write($"║0{valorNro}");     // Si es nro de una cifra agrega un 0(cero)
                }
            }
            else
            {
                Console.Write($"║{cartonBingo[fila, columna]}");    // Si es nro de 2 cifras imprime directamente
            }
        }
        Console.WriteLine("║"); // Barra vertical final para cada fila
        if (fila < 2)
        {
            Console.WriteLine("╠══╬══╬══╬══╬══╬══╬══╬══╬══╣");
        }
        else if (fila == 2)
        {
            Console.WriteLine("╚══╩══╩══╩══╩══╩══╩══╩══╩══╝");
        }
    }
    Console.WriteLine("\n¿Desea generar otro cartón? S/N");   // Iterar para generar varios cartones
    bool error = true;
    do
    {
        string respuesta = Console.ReadLine();
        if (respuesta.ToUpper() == "S")
        {
            error = false;          // Sale de este bucle y genera nuevo carton
        }
        else if (respuesta.ToUpper() == "N")
        {
            error = false;          // Sale de este bucle
            continuar = false;      // Sale del programa
        }
        else
        {
            Console.WriteLine("Opción incorrecta. Intente nuevamente: S/N");
        }
    } while (error);
    Console.Clear();
}