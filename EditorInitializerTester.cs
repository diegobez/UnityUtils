using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

////////////////////////////////////////////////////////////////////////////////
//   Placed at : UnityTestTools/tests/editor
//                                                                            //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
namespace UnityTest
{
  [TestFixture]
  [Category("Unit Tests")]

  internal class EditorInitializerTest
  {

    ////////////////////////////////////////////////////////////////////////////////
    //  Test component to check that EditorInitialized is called
    //                                                                            //  
    ////////////////////////////////////////////////////////////////////////////////
    public class TestComponent : MonoBehaviour, IEditorInitialized
    {
      public bool m_initialized = false;
      
      ////////////////////////////////////////////////////////////////////////////////
      //  Called from the editor for game initialization                            //
      //  consider using #if UNITY_EDITOR wrapping the method code                  //
      //                                                                            //  
      ////////////////////////////////////////////////////////////////////////////////
      public void EditorInitialize()
      {
        m_initialized = true;
      }
    }

    [Test]
    [Category("Editor Initializer tests")]
    [MaxTime(100)]
    public void ExceptionTest()
    {
      List <TestComponent> m_componentList = new List<TestComponent>();
      GameObject goRoot                   = new GameObject();
      GameObject goRootChild1             = new GameObject();
      GameObject goRootChild2             = new GameObject();
      GameObject goRootChild1child1       = new GameObject();
      GameObject goRootChild1child2       = new GameObject();
      GameObject goRootChild1child2child1 = new GameObject();

      goRootChild1.transform.SetParent( goRoot.transform );
      goRootChild2.transform.SetParent( goRoot.transform  );
      goRootChild1child1.transform.SetParent( goRootChild1.transform  );
      goRootChild1child2.transform.SetParent( goRootChild1.transform  );
      goRootChild1child2child1.transform.SetParent( goRootChild1child2.transform  );


      goRootChild1child2child1.name = "goRootChild1child2child1";
      goRootChild1.name = "goRootChild1";
      goRootChild2.name = "goRootChild2";

      m_componentList.Add( goRootChild1.AddComponent<TestComponent>() );
      m_componentList.Add( goRootChild2.AddComponent<TestComponent>() );
      m_componentList.Add( goRootChild2.AddComponent<TestComponent>() ); //test multiple components is a gameObject
      m_componentList.Add( goRootChild1child2.AddComponent<TestComponent>() );
      m_componentList.Add( goRootChild1child2child1.AddComponent<TestComponent>() );

      goRootChild2.SetActive( false );
      goRootChild1child2.SetActive( false );

      EditorInitializer.EditorInitialize();

      foreach ( TestComponent component in m_componentList )
      {
        Assert.IsTrue( component.m_initialized );
      }

      GameObject.DestroyImmediate( goRoot );

      Assert.Pass();
    }    
  }
}
