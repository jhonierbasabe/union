   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views  5   	       PRORAM.Views.LogsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PRORAM.Views     5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   Lcontrols9http://schemas.microsoft.com/netfx/2009/xaml/presentation    5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    MaterialDesignPaper#���   5
   	     MaterialDesignBody#���  5   	   $��Stretch=�5   	   $��Stretch=�5   	   $��Medium"�5   	   $��10(�5   	   $
��auto��5   	   $
��auto��6   .��5      � $��Stretch=�6   $��Stretch=�6'    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   ��  �� CornerRadius$ 6��6    �� Padding$ 2q�6"   $��10q�6.     PrimaryHueMidBrush#���  6:   ._�5      �� $
��Logs��6   
�������$
��Left=�66   5!      � 5#   
   $��1��6   $��5q�6   $��0��6'   .+�5%      �� $	��Top=�5&      $��Visible=�5'      ��.  $��10(�5)      $��0q�5*      $
��auto��5+      
������ ��5%      �� *	ListEvent+ �� View 5-      �� 5.      .P�5/      �� $
��Hora��6T    �� DisplayMemberBinding 6)   �� *	TimeEvent+6T   �� 50      $��Evento��6W    �� Width$	 400��6g    6)   �� *MessageEvent+6g   51      53      54      55      56      