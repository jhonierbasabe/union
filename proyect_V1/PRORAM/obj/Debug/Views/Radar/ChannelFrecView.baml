   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   "    PRORAM.Views.ChannelFrecView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   (localclr-namespace:PRORAM.Views   5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    MaterialDesignBody#���   5   	   $��Medium"�5   	   $��14(�5   	   $	��330��5   	   $	��520��6   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *LoadedCommand+5      .��5      �� 
���iii�$��1q�6$    �� CornerRadius$ 5��68     MaterialDesignPaper#���  6I   ._�5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��1* �6    .+�5!      �� $��0��6   .��5"      �� -   5        ._�5#      (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ PrimaryDark=�6/   $��35��6C      CornerRadius$ 5��6O   .��5$      ]� $
��Left=�6$   .+�5%      �� -   5        $��Title��5%   $   $"��Edición canal de frecuencia��61   
�������$
��Left=�6j   $��5,0,0,0q�5&   $   5'      5(      5)      5*      �� 5+      $��1��6   .��5,      � 	 � ColumnDefinitions	 5-      �� 5.      $��1* �6+   �� 5/      $��1* �6+   .+�51      ]� $��0��6    .+�54      �� -   5        $��Label1��54       $��Dispositivo radar��6.   $��Bottom=�6K   $��10q�6f   $
��Left=�6r   �� 55      $��Bottom=�6@   $��10,10,80,10q�6[   $
��Left=�6q   ��6    �� *SChannel.Id+6q   57      ]� 58      $��1��6    $��Stretch=�60   .+�59      �� -   5        $��Label2��59       $��Frecuencia central��6.   $��Bottom=�6K   $��10q�6f   �� 5:      $��Bottom=�6>   $��
0,10,10,10q�6Y   $	��100��6m   ��6    �� *	Frecuency+6m   5;      5<      5>      �� 5?      $��2��6   .��5@      �� $��10q�6   .+�5A      �� $ 5��6   $��1q�6.   
����d�$��10q�6^   $��45��6j   ._�5B      �� -   5        $��Label3��5B       $��Center=�6.   $
��Left=�6I   $��10,0,0,0q�6d   5C      5D      5E      �� 5F      $��3��6   .��5G      ]� .+�5H      �� -   5        $��Label4��5H      $��Bottom=�6*   $��10q�6E   �� 5I      -   5        $��
Seleccione��5J      
���iii�
 �� DisplayMemberPath$
 DisplayName��5N      $��Center=�5O      $��10q�6,   ��5L      �� *
Channels+ �� SelectedItem 5M      �� *
SChannel+ �� ValidatesOnDataErrors .  �� NotifyOnValidationError .  �� ValidatesOnExceptions .  �� UpdateSourceTrigger$ 	LostFocus=�5O   ,   5P      5Q      �� 5R      $��4��6   .��5S      �� $��10,10,10,10q�6   5T      �� 5U      $��5��6   .��5V      ]� $��Right=�6   .+�5W      �� $��10q�5X      
�������
���   ���5[      �� *CancelCommand+.��6=   
Cancelar6G   �� 5\        MaterialDesignRaisedButton#��[ 5]      $��10q�5^      ��6%   �� *SubmitCommand+.��6H   	Aceptar6Q   5_      5`      5a      5b      5c      