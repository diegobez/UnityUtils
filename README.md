# UnityUtils
Utils for Unity3D projects. These utils are not project-related, but generic utils that could be usefull to any Unity3d code base..

- EditorInitializer.cs -> Handles Unity3D editor to allow initializing our game at editor time, calling every IEditorInitialized interface that we implemented. It adds a "editor Inizialize" button at Unity editor bar ( into Edit ), and calls every implemented IEditorInitialized interface in our game code.
- IEditorInitialized.cs -> Interfaz to be implemented in our game components if we want to run some of our specific  code at editor time.
- UnityUtils.cs         -> Static utils for Unity Projects.
