   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views  5   	       PRORAM.Views.LayersView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:PRORAM.Views     5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . 
������    MaterialDesignBody#���   5   	   $��Medium"�5   	   $��14(�5   	   $
��auto��5      $
��auto��6   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadScreenCommand+5       � 	Resources 5        ComboBoxToggleButton �� 
TargetType � ColumnDefinitions'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup	   States   System.Windows.VisualState
   
Storyboard �� TargetProperty �� 
TargetName R� KeyTime	  System.Windows.Visibility 	  Hidden R� Value 	  Visible ��Name �� CornerRadius �� EndPoint �� 
StartPoint  BorderLightColor � Color � Offset  BorderDarkColor  ControlLightColor  ControlMediumColor �� Color F�Name��%�  &	       �� 5   
   " d�5      .R�5      �  5      �� 5      �� 5      $��20 �6'    5        5       CheckStates��.	 5!        Checked��.
 5"      �� .��5#   "   T� $ (UIElement.Visibility)�6@   $ 	PowerDown��5$   ,   ..�5%   &   i� $ 0��6=   # Z 5&   '   5'   #   T� 5(   "   $ (UIElement.Visibility)�6@   $ PowerUp��5)   ,   ..�5*   &   i� $ 0��6=   # Z 5+   '   5,   #   5-      5.        5/      	Unchecked��6&     50      Indeterminate��6&   51      .+�53      �� $ Border��6   $��2��54      $ 2��55      $��1q�56      ��57      �� 58      $	 0,1&�6.   $	 0,0&�59      .��5:      � # �  6+   $ 0Q�5;      � 5<      # �  6+   $ 1Q�5=      5>      ��5@      �� 5B      $	 0,0&�6.   $	 0,1&�5C      ��5E   "   � � 5F   &   # �  63   � 5G   &   # �  63   $	 1.0Q�5H      5I   #   5K      5N      �� 5O      $��0��6   $ 2,0,0,2��5P      $��1q�5Q      ��5R      �� 5S      # �  6*   5U      F� 5V      $ 	PowerDown��6   $��Center=�5W      $��Center=�5X      ����� � �� � � �� ��v �. �.c @iG ����(\O0@�@� � ���� ��!@�� ��!@T� � � ��  ��j ��j Ѐ2  	= р� �iP Q��L ������ @�@KL P(� @KL ��� @KL @T� �@KL  �� �s^ 0��  z  �� � z @f�@T� �� ��� ������O� -1�)<р2  	= �@T�  -1�@T� @o@׀�� ��O р�� ��O���� @o@ ����  -1�@T�  -1	��5Z      �� 5[      $ 	LightGray��6*   5]      F� 5^      $ PowerUp��6   $��Center=�5_      $��Center=�5`      $y�	Collapsed=�5a      ����� � �� �@KL @T�  ��j ��j �@KL  �� �s^ 0��  z  �� � z @f�@T� �� ���� ��� $� @f � $�  �� �P�0�� ��! �� ��!@T� � � ��  ��j ��j �@T� @o@׀�� ��O р�� ��O���� @o@ ����  -1�@T�  -1�@T� @o@	��5c      �� 5d      $ Green��6*   5f      5g      5h      5i      .��5j      �� 
���iii�$��1q�6#   $ 5��67     MaterialDesignPaper#���  6H   ._�5k   
   �  � RowDefinitions 5l      �� 5m      $
��auto �6    �� 5n      $
��auto �6    �� 5o      $
��auto �6    �� 5p      $
��auto �6    �� 5q      $
��auto �6    .+�5t      �� $��0��6   .��5u      �� -   5        ._�5v      (
  "MaterialDesignThemes.Wpf.ColorZone
   
  Mode$ PrimaryDark=�6/   $��35��6C    
  CornerRadius$ 5��6O   .��5w      �� -   5        $��Title��5w       $��10,0,0,0q�6-   
�������.��5x   3   Configuración de capas6L   5y      5z      5{      �� 5|      $��1��6   .��5}      &   System.Windows.Controls.DataGrid  $
��auto��5~      $��10q�6)      AutoGenerateColumns .  $
��auto��5�         AlternatingRowBackground
 �����   CanUserAddRows .  
���iii���5}      �� *
LayerModel+ �� Mode$ TwoWay=�  �� SelectedItem  5~   5   �� *SLayerModel+!   Columns! 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn"   Header$" Id��6-   #   Width5  /System.Windows.Controls.DataGridLengthConverter$
# auto 69   $   
IsReadOnly$ . 1  +System.Windows.Controls.DataGridBoundColumn%   Binding% 6G   �� & �� Path$& Id�6c     5�      $" Nombre��6-   $
# auto 6=   $ . % 6J   �� $
& Name�6h   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $
# auto 61   $" Mostrar capa��6>   $ . '   CellTemplate' 5�      �� 5�   "   .R�5�   &   �� .+�5�   *   d� $��Center=�5�   7   $��Center=�6R   ( d� 	IsChecked( 5�   7   �� *State+$ TwoWay=�) �� UpdateSourceTrigger$) PropertyChanged=���5�   o   �� $%& DataContext.UpdateLayersCommand�* �� RelativeSource* �� + �� AncestorType$+ DataGridL�5�   +   5�   '   5�   #   5�      5�      5�      �� 5�      $��2��6   $��5,0,5,0q�6%   �� 5�      $��4��6   $��10q�6*   .��5�      ]� $��Right=�6   .+�5�      �� 
�������
���   ���6G   �� *CancelCommand+.��6i   Cerrar6q   5�      5�      5�      5�      5�      