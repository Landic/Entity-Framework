   M S B A M L   `   `   `  ����I  EAuthorAndBooks, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:AuthorAndBooks.ViewAuthorAndBooks.View  5   	   !    AuthorAndBooks.View.AddBook   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:AuthorAndBooks.View     5   	     � Title$  AddBook��5   	   $	��220��6   $	��400��6)   $	��220��68   $	��400��6H   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.5* �6   �� 5      �� 5      $
��0.5* �6   �� 5      �� 5      .+�5   
   �� $#��Выберите автора��6   6,   �� 5   
   $��1��6   $��10q�6     �� DisplayMemberPath$
 Name��6s   ��6,   �� *Author+ �� SelectedItem 6K   �� *SelectedAuthor+6�   �� 5   
   $��Введите книгу��6   $��2��6(   67   �� 5   
   $��3��6M   $��10q�6Z   ��6   �� *Name+ �� UpdateSourceTrigger$ PropertyChanged=�6h   � 5   
   $��4��6    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��Отмена��67   $��10q�6H   ��6   �� *CancelCommand+6V   �� 5      $��Добавить��64   $��1��6G   $��10q�6W   ��6   �� *
AddCommand+6e   5      5      5      