   M S B A M L   `   `   `  ����D  @ITCompany, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ITCompany	ITCompany  5   	       ITCompany.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ITCompany     5   	     � Title$  
IT Company��5   	   $	��450��6   $	��800��6)   $	��450��65   $	��800��6E   .��5	      �  � RowDefinitions 5
   
   �� 5      $��0.08* �6   �� 5      $
��0.1* �6   �� 5      .+�5   
   o� .@�5      m� $��Операции��6   .@�5      m� $��Работники��6   .@�5      m� $)��#Добавить работника��6    m� Command 6;   �� *AddEmployeeCommand+6d   m� 5      $'��!Удалить работника��6    6:   �� *DeleteEmployeeCommand+6f   m� 5      $)��#Изменить работника��6    6;   �� *EditEmployeeCommand+6e   5      m� 5      $��Должности��6   .@�5      m� $)��#Добавить должность��6    6;   �� *AddPositionCommand+6d   m� 5      $'��!Удалить должность��6    6:   �� *DeletePositionCommand+6f   m� 5      $)��#Изменить должность��6    6;   �� *EditPositionCommand+6e   5      5      5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      �� 5       $
��0.1* �6#   .+�5"      �� $��	10 5 10 5q�6   ��6)   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�6m   �� 5#      $��	10 5 10 5q�6   $��1��6(   ��68   �� *SearchCommand+.��6Z   
Поиск6a   5$      &   System.Windows.Controls.DataGrid  5%   
      AutoGenerateColumns$ False��64   $��2��6P      
IsReadOnly$
 True��6]   $��	10 5 10 5q�6o   ��6   �� *
Employee+ �� SelectedItem 6�   �� *SelectedEmployees+   Columns 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Id��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 61   �� *Id+  5(      $
	 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 6L   
 63   �� *Name+6L     5)      $	 Surname��6%   $	 200 6R   
 66   �� *	Surname+6R     5*      $	 Email��6%   $	 200 6N   
 64   �� *Email+6N     5+      $	 Position��6%   $	 155 6Y   
 67   �� *Position.Name+6Y   5-      5.      5/      