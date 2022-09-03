using System;
using System.Collections.Generic;

namespace _6314110007
{

    // Basic node stored in a linked list
    class ListNode<AnyType>
    {
        // Constructors
        public ListNode( AnyType theElement ) : this( theElement, null )
        {
        }

        public ListNode( AnyType theElement, ListNode<AnyType> n )
        {
            element = theElement;
            next = n;
        }

        public AnyType element;
        public ListNode<AnyType> next;
    }


    // LinkedList class
    //
    // Linked list implementation of the list
    //    using a header node.
    // Access to the list is via LinkedListIterator.

    // CONSTRUCTION: with no initializer
    // Access is via LinkedListIterator class
    //
    // ******************PUBLIC OPERATIONS*********************
    // bool IsEmpty( )        --> Return true if empty; else false
    // void MakeEmpty( )      --> Remove all items
    // LinkedListIterator Zeroth( )
    //                        --> Return position to prior to first
    // LinkedListIterator First( )
    //                        --> Return first position
    // void Insert( x, p )    --> Insert x after current iterator position p
    // void Remove( x )       --> Remove x
    // LinkedListIterator Find( x )
    //                        --> Return position that views x
    // LinkedListIterator FindPrevious( x )
    //                        --> Return position prior to x
    // ******************ERRORS********************************
    // No special errors

    public class LinkedList<AnyType>
    {
        // Construct the list.
        public LinkedList( )
        {
            header = new ListNode<AnyType>( default );
        }

        // Returns true if empty, false otherwise.
        public bool IsEmpty( )
        {
            return header.next == null;
        }

        // Makes the list logically empty.
        public void MakeEmpty( )
        {
            header.next = null;
        }

        // Returns an iterator representing the header node.
        public LinkedListIterator<AnyType> Zeroth( )
        {
            return new LinkedListIterator<AnyType>( header );
        }

        // Returns an iterator representing the first node in the list.
        // This operation is valid for empty lists.
        public LinkedListIterator<AnyType> First( )
        {
            return new LinkedListIterator<AnyType>( header.next );
        }

        // Inserts after p.
        // x is the item to insert.
        // p is the position prior to the newly inserted item.
        public virtual void Insert( AnyType x, LinkedListIterator<AnyType> p )
        {
            if( p != null && p.current != null )
                p.current.next = new ListNode<AnyType>( x, p.current.next );
        }

        // Returns iterator corresponding to the first node containing an item;
        // iterator is not valid if item is not found.
        // x is the item to search for.
        public LinkedListIterator<AnyType> Find( AnyType x )
        {
            ListNode<AnyType> itr = header.next;

            while( itr != null && !itr.element.Equals( x ) )
                itr = itr.next;

            return new LinkedListIterator<AnyType>( itr );
        }

        // Return iterator prior to the first node containing an item.
        // x is the item to search for.
        // Returns appropriate iterator if the item is found. Otherwise, the
        // iterator corresponding to the last element in the list is returned.
        public LinkedListIterator<AnyType> FindPrevious( AnyType x )
        {
            ListNode<AnyType> itr = header;

            while( itr.next != null && !itr.next.element.Equals( x ) )
                itr = itr.next;

            return new LinkedListIterator<AnyType>( itr );
        }

        // Removes the first occurrence of x.
        public void Remove( AnyType x )
        {
            LinkedListIterator<AnyType> p = FindPrevious( x );

            if( p.current.next != null )
                p.current.next = p.current.next.next;  // Bypass deleted node
        }

        private ListNode<AnyType> header;
    }

    // LinkedListIterator class; maintains "current position"
    //
    // Linked list implementation of the list iterator
    //   using a header node.
    // CONSTRUCTION: Internal only, with a ListNode
    //
    // ******************PUBLIC OPERATIONS*********************
    // void advance( )        --> Advance
    // boolean isValid( )     --> True if at valid position in list
    // AnyType retrieve       --> Return item in current position

    /**

    * @author Mark Allen Weiss
    * @see LinkedList
    */

















































































































































    public class LinkedListIterator<AnyType>
    {
        // Construct the list iterator
        // theNode is any node in the linked list.
        internal LinkedListIterator( ListNode<AnyType> theNode )
        {
            current = theNode;
        }

        // Returns true if the current position is a valid position in the list.
        public bool IsValid( )
        {
            return current != null;
        }

        // Return the stored item or AnyType.default if the current position
        // is not in the list.
        public AnyType Retrieve( )
        {
            return IsValid( ) ? current.element : default;
        }

        /**
        * Advance the current position to the next node in the list.
        * If the current position is null, then do nothing.
        */











































































































































































        public void Advance( )
        {
            if( IsValid( ) )
                current = current.next;
        }

        internal ListNode<AnyType> current;    // Current position
    }
}

