   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   !    PRORAM.Views.PowerRadarView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   (viewsclr-namespace:PRORAM.Views   5   	   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006   5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . 
������    MaterialDesignBody#���   5   	   $��Medium"�5   	   $��14(�5   	   $	��330��5   	   $	��500��6   .��5      �� 
���iii�$��1q�6#    �� CornerRadius$ 5��67     MaterialDesignPaper#���  6H   ._�5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��1* �6    .+�5      �� $��0��6   .��5      �� -   5        ._�5      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6/   $��35��6C      CornerRadius$ 5��6O   .��5      ]� $
��Left=�6$   .+�5       �� $��Edición de potencia��6$   
�������$
��Left=�6U   $��5,0,0,0q�6p   6�   5!      5"      5#      5$      �� 5%      $��1��6   .��5&      ]� .+�5'      �� -   5        $��Label1��5'      $��Bottom=�6+   $��10q�6F   �� 5(      -   5        $��Disposivito��5(      $��Bottom=�6M   $��10q�6h   ��6/   �� *	NameRadar+6h   5)      5*      �� 5+      $��2��6   .��5,      �� $��10q�6   .+�5-      �� $ 5��6   $��1q�6.   
����d�$��10q�6^   $��45��6j   ._�5.      �� -   5        $��Label2��5.       $��Center=�6.   $
��Left=�6I   $��10,0,0,0q�6d   5/      50      51      �� 52      $��3��6   .��53      ]� .+�54      �� -   5        $��Label3��54      $��Bottom=�6*   $��10q�6E   �� 55      .+�56      �� -   5        $��Label4��56       $��Center=�57   M    �� Target 56   .   ��  �� ElementName$ Power����57       �� *	TXPower+ �� StringFormat$ N2��6M   �� 58      -   5        $��Power��58   !   $��10q�6.    �� TickFrequency$ 10Q�59      	 �� Orientation$	 
Horizontal=�5:      
 �� TickPlacement$
 BottomRight=�5;       � ToolTip$" MaterialDesignDiscreteSlider��5<       � Minimum$ 0Q�5=       � Maximum$	 100Q�5>      $	��280��5@       � Value 5?      �� *	TXPower+ �� Mode$ TwoWay=�5@      5A      5B      5C      �� 5D      $��4��6   .��5E      �� $��10,10,10,10q�6   5F      �� 5G      $��5��6   .��5H      ]� $��Right=�6   .+�5I      �� $��10q�5J      
�������
���   ���5M      �� *CancelCommand+.��6=   
Cancelar6G   �� 5N        MaterialDesignRaisedButton#��[ 5O      $��10q�5P      ��6%   �� *SubmitCommand+.��6H   	Aceptar6Q   5Q      5R      5S      5T      5U      