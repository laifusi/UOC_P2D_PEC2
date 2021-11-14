# PEC 2 - Un juego de plataformas

## Cómo jugar
El objetivo del juego es llegar a la bandera al final del nivel, conseguir el mayor número de puntos y de monedas y hacerlo en el menor tiempo posible, evitando que los enemigos que toquen y evitando caer al vacío.

En las versiones de Windows y Web, el jugador debe usar las teclas "A" y "D" para mover al personaje, y la tecla "espacio" para saltar. Mientras que en la version para Android, usará un joystick situado a la derecha de la pantalla, evitando así tapar al personaje con la mano.

Si los enemigos te tocan, mueres o pierdes tu estado super, pero si, en cambio, saltas sobre ellos, son estos los que mueren.

Las setas te proporcionan el estado super, con el que consigues una vida extra, además de una gran cantidad de puntos.

Los bloques de exclamación contienen monedas o setas, mientras que los bloques normales pueden contener una cantidad aleatoria monedas, que puede ser 0. Estos, además, se pueden destruir si los golpeamos en modo super.

Dependiendo de la altura a la que lleguemos a la bandera, se ganarán unos puntos o otros, siendo la punta superior de la bandera la que más puntos proporciona y la parte inferior, la que menos.

Todos los puntos están basados en el sistema de puntuación del juego original.

## Estructura e implementación
En primer lugar, tenemos la clase Player, que se encarga de controlar todo el movimiento del personaje, además de su vida, su estado super y su colisión con setas o con una pared invisible que hemos añadido para evitar que el jugador se mueva demasiado hacia la izquierda. La cámara, por su parte, se moverá en relación a la posición del jugador, mediante la clase CameraMovement, únicamente en el eje x.

Por otro lado, la clase Enemy se encarga del moviemiento de este y la detección de colisiones con el jugador, comprobando el punto de colisión para decidir cuál de los dos recibe daño. El enemigo, además, controla si va chocar con una pared, mediante Raycast, y gira si es así.

En cuanto a los puntos, las monedas y el tiempo, los hemos estructurado en dos clases, un Manager, que se encarga de controlar la cantidad de puntos, monedas o tiempo, y un TextUpdater, que se encarga de actualizar el texto del HUD cada vez que se añade una moneda, puntos o un segundo. Para esto último, hemos implementado una clase abstracta TextUpdater que define el método que se encargará de cambiar el texto, pero que necesita estar suscrito a una Action<int> que defineremos en cada una de las clases que heredan de TextUpdater, es decir, PointsTextUpdater, CoinsTextUpdater y TimeTextUpdater. De este modo, cada una de ellas se suscribe a la Action del Manager que le corresponde, PointsManager, CoinsManager y TimeManager. Por otro lado, para actualizar el highscore, creamos una clase HighscoreText que tendrá definido el tipo de puntuación que debe mostrar y sacará dicha puntuación de PlayerPrefs. Hemos definido los tipos de puntuación mediante un enum.

Los bloques, tanto de exclamación, como normales, se han definido mediante una clase abstracta que hemos llamado HittableFromBelow. En esta definimos las acciones comunes de ambas clases, es decir, la detección de la colisión desde abajo y la salida de prefabs de los bloques. Definimos, además, un método abstracto que cada clase deberá implementar y que determina qué hacer cuando tocamos el bloque desde abajo. En el caso de la clase BreakableBox, dependiendo de si el jugador está en modo super se romperá el bloque o se sacará una moneda de él. En el caso de la clase ExclamationBox, se sacará el prefab y se marcará el bloque como usado.

La clase Flag se encarga de controlar la altura a la que el jugador alcanza la bandera y de enviar los puntos correspondientes a la clase PointsManager.

La clase WinLoseManager, por su parte, se encarga de detectar cuando ha acabado el juego y actuar en consecuencia, mostrando la pantalla de pérdida cuando el jugador muere, y la pantalla de ganado cuando gana. En esta, además, se encarga de actualizar las máximas puntuaciones, controlando si los puntos y las monedas son mayores a la puntuación guardada en PlayerPrefs, y si el tiempo es menor al guardado, pero teniendo en cuenta que no puede ser 0.

Finalmente, hemos añadido algunas clases para controlar la música, haciendo uso del patrón singleton para mantener siempre el mismo objeto; los elementos que solo deberían mostrarse en android o en ordenador, como el joystick o las intrucciones; la muerte al caer al vacío, los cambios de escenas o la salida de la aplicación y la desaparición de las instrucciones después de unos segundos.

## Sprites y sonidos
Para esta PAC hemos utilizado el [Platformer Art: Pixel Redux](https://www.kenney.nl/assets/platformer-art-pixel-redux) de [Kenney](https://www.kenney.nl/). Además, hemos utilizado los Tilemaps para la creación del nivel, con Tilemaps distintos para los elementos del fondo, los elementos de juego y los elementos en primer plano. Se ha usado, también los Prefab Brush para añadir desde el Tile Palette a los enemigos, los bloques, las monedas e incluso la bandera.

Los efectos de sonido se han hecho mediante el programa Bfxr.

La música de fondo es el [Red Heels (piano version)](https://opengameart.org/content/red-heels-piano-ver) de [TAD](https://opengameart.org/users/tad).

## Builds
Se han hecho builds tanto para Windows, como para WebGL y Android. Para este último, además, se ha usado el [Joystick Pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631) de [Fenerax Studios](https://assetstore.unity.com/publishers/32730).

Los tres builds se pueden encontrar [aquí](https://fuscor.itch.io/p2d-pec-2-un-juego-de-plataformas).

## Dificultades y problemas
La mayor dificultad encontrada ha sido el control del salto del personaje al intentar añadir la opción de saltos más largos o más cortos.

Se han encontrado otros pequeños problemas por el camino, pero se han encontrado soluciones fácilmente. Un ejemplo de ello, es la imposibilidad de desactivar el collider de las monedas recogidas desde el código en la corrutina. Para solucionarlo, se ha añadido un bool que define si la moneda se ha cogido o no, evitando que el personaje coja una misma moneda más de una vez.

Por otro lado, el movimiento en la versión Android se ha hecho con poco tiempo y resulta bastante complicado de controlar a la hora de jugar. Esto puede deberse a la posición del joystick o a la implementación utilizada.

## Vídeo
![](PEC2_video.mp4)



