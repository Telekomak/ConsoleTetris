**Class Square:**
    Main class - Game field and Objects contributes out of it
    Has properties: `string Character`, `Consolecolor Color`, `Attribute`
        Attribute: Determines collisions between objects and game field
------------------------------------------------------------------------------
**Class Object:**
    Loads files, contains all shapes rotations and other informations
    Has properties: `Shape`, `Square[,,] Data`, `Square[,] ActualRot`
    Has methods: Square[,] Rotate(), Square[,,] LoadObject
------------------------------------------------------------------------------
**Class Game**
    Contains game cotrol methods and game field
    Has properties: Square[,] Field, Object Player
    Has methods: `Square[,] LoadField`, `void Writefield()`,
    `void WriteToField()`, `void Move()`, `void GameOver()`,
    `void AbortPLayer()`, `bool IsSticky()`