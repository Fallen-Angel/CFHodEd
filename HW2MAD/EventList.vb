''' <summary>
''' Event based generic list.
''' </summary>
Friend NotInheritable Class EventList(Of T)
 Implements IList(Of T)

 ''' <summary>Internal list.</summary>
 Private m_List As New List(Of T)

 ''' <summary>
 ''' Raised when an object has been added to the end of list.
 ''' </summary>
 Public Event AddItem()

 ''' <summary>
 ''' Raised when an object has been inserted in the list.
 ''' </summary>
 ''' <param name="index">
 ''' Index where the object has been inserted.
 ''' </param>
 ''' <remarks>
 ''' An event is fired only if the action is successful.
 ''' </remarks>
 Public Event InsertItem(ByVal index As Integer)

 ''' <summary>
 ''' Raised when an object has been modified (i.e. assigned) in the list.
 ''' </summary>
 ''' <param name="index">
 ''' Index where the object has been modified.
 ''' </param>
 ''' <remarks>
 ''' An event is fired only if the action is successful.
 ''' </remarks>
 Public Event ModifiedItem(ByVal index As Integer)

 ''' <summary>
 ''' Raised when an object is about to be removed from the list.
 ''' </summary>
 ''' <param name="index">
 ''' Index where of the object which will be removed.
 ''' </param>
 ''' <remarks>
 ''' An event is fired only if the action is successful.
 ''' </remarks>
 Public Event PreRemoveItem(ByVal index As Integer)

 ''' <summary>
 ''' Raised when an object has been removed from the list.
 ''' </summary>
 ''' <param name="index">
 ''' Index where the object used to be.
 ''' </param>
 ''' <remarks>
 ''' An event is fired only if the action is successful.
 ''' </remarks>
 Public Event RemoveItem(ByVal index As Integer)

 ''' <summary>
 ''' Raised when the list about to be cleared using the Clear() method.
 ''' </summary>
 Public Event PreClearList()

 ''' <summary>
 ''' Raised when the list is cleared using the Clear() method.
 ''' </summary>
 Public Event ClearList()

 ''' <summary>
 ''' Adds an item to the end of the list.
 ''' </summary>
 Public Sub Add(ByVal item As T) Implements System.Collections.Generic.ICollection(Of T).Add
  If item Is Nothing Then _
   Throw New ArgumentNullException("item") _
 : Exit Sub

  m_List.Add(item)
  RaiseEvent AddItem()

 End Sub

 ''' <summary>
 ''' Clears the list.
 ''' </summary>
 Public Sub Clear() Implements System.Collections.Generic.ICollection(Of T).Clear
  m_List.Clear()
  RaiseEvent ClearList()

 End Sub

 ''' <summary>
 ''' Returns whether an item is present in the list.
 ''' </summary>
 Public Function Contains(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Contains
  Return m_List.Contains(item)

 End Function

 ''' <summary>
 ''' Copies the list to an array.
 ''' </summary>
 Public Sub CopyTo(ByVal array() As T, ByVal arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of T).CopyTo
  m_List.CopyTo(array)

 End Sub

 ''' <summary>
 ''' Returns the number of items in the list.
 ''' </summary>
 Public ReadOnly Property Count() As Integer Implements System.Collections.Generic.ICollection(Of T).Count
  Get
   Return m_List.Count()

  End Get

 End Property

 ''' <summary>
 ''' Returns if the list is read-only.
 ''' </summary>
 Private ReadOnly Property IsReadOnly() As Boolean Implements System.Collections.Generic.ICollection(Of T).IsReadOnly
  Get
   Return CType(m_List, ICollection(Of T)).IsReadOnly

  End Get

 End Property

 ''' <summary>
 ''' Removes an item from the list.
 ''' </summary>
 Public Function Remove(ByVal item As T) As Boolean Implements System.Collections.Generic.ICollection(Of T).Remove
  Dim index As Integer = m_List.IndexOf(item)
  Dim result As Boolean = m_List.Remove(item)

  If result Then _
   RaiseEvent RemoveItem(index)

  Return result

 End Function

 ''' <summary>
 ''' Returns the list enumerator.
 ''' </summary>
 Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of T) Implements System.Collections.Generic.IEnumerable(Of T).GetEnumerator
  Return m_List.GetEnumerator()

 End Function

 ''' <summary>
 ''' Returns the index of the specified item.
 ''' </summary>
 Public Function IndexOf(ByVal item As T) As Integer Implements System.Collections.Generic.IList(Of T).IndexOf
  Return m_List.IndexOf(item)

 End Function

 ''' <summary>
 ''' Inserts the specified item in the specified position.
 ''' </summary>
 Public Sub Insert(ByVal index As Integer, ByVal item As T) Implements System.Collections.Generic.IList(Of T).Insert
  If item Is Nothing Then _
   Throw New ArgumentNullException("item") _
 : Exit Sub

  m_List.Insert(index, item)

  If (index >= 0) AndAlso (index < m_List.Count) Then _
   RaiseEvent InsertItem(index)

 End Sub

 ''' <summary>
 ''' Returns the specified item.
 ''' </summary>
 Default Public Property Item(ByVal index As Integer) As T Implements System.Collections.Generic.IList(Of T).Item
  Get
   Return m_List(index)

  End Get

  Set(ByVal value As T)
   m_List(index) = value

   If (index >= 0) AndAlso (index < m_List.Count) Then _
    RaiseEvent ModifiedItem(index)

  End Set

 End Property

 ''' <summary>
 ''' Removes the item at the specified position.
 ''' </summary>
 Public Sub RemoveAt(ByVal index As Integer) Implements System.Collections.Generic.IList(Of T).RemoveAt
  Dim item As T
  Dim valid As Boolean = (index >= 0) AndAlso (index < m_List.Count)

  If valid Then _
   item = m_List(index)

  m_List.RemoveAt(index)

  If valid Then _
   RaiseEvent RemoveItem(index)

 End Sub

 ''' <summary>
 ''' Sorts the list. Does not fire any events.
 ''' </summary>
 Public Sub Sort()
  m_List.Sort()

 End Sub

 ''' <summary>
 ''' Returns the non-generic enumerator.
 ''' </summary>
 Private Function GetNonGenericEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
  Return CType(m_List, IEnumerable).GetEnumerator()

 End Function

 ''' <summary>
 ''' Copies all elements of this list to a new array.
 ''' </summary>
 Public Function ToArray() As T()
  Return m_List.ToArray()

 End Function

End Class
