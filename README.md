**Class Square:**
    **Description:** Base class - Game field and Objects contributes out of `Squares`
    **Has properties:** `string Character`, `Consolecolor Color`,`Attribute`
    `Attribute`: Determines collisions between objects and game field

**Class Object:**
    -Description: Loads files, contains all shapes rotations and other informations
    -Has properties: `Shape`, `Square[,,] Data`, `Square[,] ActualRot`
    -Contains methods: `Square[,] Rotate()`, `Square[,,] LoadObject`

**Class Game**
    -Description: Contains game cotrol methods and game field
    -Has properties: Square[,] Field, Object Player
    -Contains methods: `Square[,] LoadField`, `void Writefield()`,
    `void WriteToField()`, `void Move()`, `void GameOver()`,
    `void AbortPLayer()`, `bool IsSticky()`