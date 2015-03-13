#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

///////////////////////////////////////////////////////////////////////////////
//                                                                           //
//   static utils for Unity Projects                                         //
//                                                                           //  
///////////////////////////////////////////////////////////////////////////////

public class UnityUtils  
{

  ////////////////////////////////////////////////////////////////////////////////
  //                                                                            // 
  //  Returns one T type component. In the game there must be only one T        //
  //   component                                                                //
  //  Works for activated and deactivated components.                           //  
  //                                                                            //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  static public T SlowFindAllComponent<T>()
  {
    List<T> foundListOneElement = SlowFindAllComponents<T>();
    CheckSlowFindAll( foundListOneElement );
    return foundListOneElement[0];
  }

  ////////////////////////////////////////////////////////////////////////////////
  //                                                                            //
  //  Returns a list with every T type component in the current editor escene,  //
  //  Both activated and deactivated components.                                //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  static public List<T> SlowFindAllComponents<T>() 
  {
    List<T> objectsInScene= new List<T>();

    CheckSlowFindAll<T>( null );

    foreach ( GameObject go in Resources.FindObjectsOfTypeAll<GameObject>() ) 
    {
      T[] components;
      if ( go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave )
        continue;
      
      string assetPath = AssetDatabase.GetAssetPath( go.transform.root.gameObject );
      if (! string.IsNullOrEmpty(assetPath))
        continue;
      
      components = go.GetComponents<T>();
      foreach ( T component in components )
      {
        if ( component != null )
        {
          objectsInScene.Add( component );
        }
      }
    }    
    return objectsInScene;
  }

  ////////////////////////////////////////////////////////////////////////////////
  //                                                                            // 
  //  Returns the gameObject called name. In the game there must be only one    //
  //  gameObject called Name                                                    //
  //  Works for activated and deactivated components.                           //  
  //                                                                            //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  static public GameObject SlowFindAllObject( string name )
  {
    List<GameObject> foundListOneElement = SlowFindAllObjects( name );
    CheckSlowFindAll( foundListOneElement );
    return foundListOneElement[0];
  }

  /////////////////////////////////////////////////////////////////////////////////
  //                                                                             //
  //  Returns a list with every gameobject with a given name in the current editor// 
  //  scene.                                                                     //
  //  Both activated and deactivated components.                                 //
  //                                                                             //  
  ////////////////////////////////////////////////////////////////////////////////
  static public List<GameObject> SlowFindAllObjects( string name ) 
  {
    List<GameObject> objectsInScene= new List<GameObject>();
    
    CheckSlowFindAll<GameObject>( null );
    
    foreach ( GameObject go in Resources.FindObjectsOfTypeAll<GameObject>() ) 
    {
      if ( go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave )
        continue;
      
      string assetPath = AssetDatabase.GetAssetPath( go.transform.root.gameObject );
      if (! string.IsNullOrEmpty(assetPath))
        continue;

      if ( go.name.Equals( name ) )
      {
        objectsInScene.Add( go );
      }
    }    
    return objectsInScene;
  }

  ////////////////////////////////////////////////////////////////////////////////
  //                                                                            //
  //  Checks and asserts for the searchs                                        //
  //                                                                            //  
  ////////////////////////////////////////////////////////////////////////////////
  static private void CheckSlowFindAll<T>( List<T> oneElementList )
  {
    #if UNITY_EDITOR
    if ( Application.isPlaying )
    {
      Debug.LogWarning(" slow Find called at running time. Type " + typeof( T ).ToString() );
    }
    #endif

    if ( ( oneElementList != null ) && ( oneElementList.Count != 1 ) )
    {
      Debug.LogError(" SlowFind didnt find exactly one item, but " + oneElementList.Count );
      Debug.LogError(" Type : " + ( (oneElementList.Count > 0 )? typeof( T ).ToString() : "" ) );
    }
  }

}
#endif
