   M S B A M L   `   `   `  ����A  =PRORAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PRORAM.ViewsPRORAM.Views  5   	   !    PRORAM.Views.SidePanelsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   (localclr-namespace:PRORAM.Views   5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   Lcontrols9http://schemas.microsoft.com/netfx/2009/xaml/presentation    5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .    MaterialDesignPaper#���   5
   	     MaterialDesignBody#���  5   	   $��Stretch=�5   	   $��Stretch=�5   	   $��Medium"�5   	   $��10(�5   	   $
��auto��5   	   $
��auto��6   .  (System.Windows.Interactivity.Interaction   Triggers 5      F  @Prism.Interactivity.InteractionRequest.InteractionRequestTrigger  5   
   3  -System.Windows.Interactivity.EventTriggerBase   SourceObject 6*   �� *'%RadarConfigurationNotificationRequest+.  (System.Windows.Interactivity.TriggerBase   Actions. 5      +  %Prism.Interactivity.PopupWindowAction     IsModal . !   CenterOverAssociatedObject .    WindowStyle 5      �� 5       �� 
TargetType" �6   .�5      ��  �� Property �@�SizeToContent	 �� Value$	 Height=�6:   �� 5       � ��$		 720��62   �� 5       �@�WindowStyle$
	 None=�68   �� 5       �@�AllowsTransparency	 . 6?   �� 5       � ��
	 ���� 67   5       
   WindowContent
 5"      )   #PRORAM.Views.RadarConfigurationView  5#      5%      5&        5(   
    6*   �� *CustomPopupRequest+. 5)         .  .  5*      �� 5+      " �6   .�5,      ��  �@�SizeToContent$	 WidthAndHeight=�6:   �� 5-       �@�
ResizeMode$	 NoResize=�67   �� 5.       �@�WindowStyle$
	 None=�68   �� 5/       �@�AllowsTransparency	 . 6?   �� 50       � ��
	 ���� 67   51      
 53      "   PRORAM.Views.CustomPopupView  54      56      57       � 	Resources 5<        ComboBoxToggleButton �� 
TargetType � ColumnDefinitions'	  !System.Windows.VisualStateManager 	  VisualStateGroups%
  System.Windows.VisualStateGroup 
  States   System.Windows.VisualState   
Storyboard �� TargetProperty �� 
TargetName R� KeyTime  System.Windows.Visibility   Hidden R� Value   Visible ��Name �� CornerRadius �� EndPoint �� 
StartPoint  BorderLightColor � Color � Offset  BorderDarkColor  ControlLightColor  ControlMediumColor �� Color F�Name  ExpanderToggleButton � RowDefinitions  ��Name! � OverridesDefaultStyle" d� 	IsChecked# �� Mode$ �� RelativeSource  BorderMediumColor 	 ContentAreaColorDark% �� Triggers& P� Property' P� Value( �� 
TargetName) �� ElementName��%s  &	       &	 �    '9�     7  �� 5?   
   " d�5@      .R�5A      �  5B      �� 5C      �� 5D      $��20 �6'    5F      
  5G      CheckStates��. 5H        Checked��. 5I      �� .��5J   "   T� $ (UIElement.Visibility)�6@   $ 	PowerDown��5K   ,   ..�5L   &   i� $ 0��6=   # Z 5M   '   5N   #   T� 5O   "   $ (UIElement.Visibility)�6@   $ PowerUp��5P   ,   ..�5Q   &   i� $ 0��6=   # Z 5R   '   5S   #   5T      5U        5V      	Unchecked��6&     5W      Indeterminate��6&   5X      .+�5Z      �� $ Border��6   $��2��5[      $ 2��5\      $��1q�5]      ��5^      �� 5_      $	 0,1&�6.   $	 0,0&�5`      .��5a      � # �  6+   $ 0Q�5b      � 5c      # �  6+   $ 1Q�5d      5e      ��5g      �� 5i      $	 0,0&�6.   $	 0,1&�5j      ��5l   "   � � 5m   &   # �  63   � 5n   &   # �  63   $	 1.0Q�5o      5p   #   5r      5u      �� 5v      $��0��6   $ 2,0,0,2��5w      $��1q�5x      ��5y      �� 5z      # �  6*   5|      F� 5}      $ 	PowerDown��6   $��Center=�5~      $��Center=�5      ����� � �� � � �� ��v �. �.c @iG ����(\O0@�@� � ���� ��!@�� ��!@T� � � ��  ��j ��j Ѐ2  	= р� �iP Q��L ������ @�@KL P(� @KL ��� @KL @T� �@KL  �� �s^ 0��  z  �� � z @f�@T� �� ��� ������O� -1�)<р2  	= �@T�  -1�@T� @o@׀�� ��O р�� ��O���� @o@ ����  -1�@T�  -1	��5�      �� 5�      $ 	LightGray��6*   5�      F� 5�      $ PowerUp��6   $��Center=�5�      $��Center=�5�      $y�	Collapsed=�5�      ����� � �� �@KL @T�  ��j ��j �@KL  �� �s^ 0��  z  �� � z @f�@T� �� ���� ��� $� @f � $�  �� �P�0�� ��! �� ��!@T� � � ��  ��j ��j �@T� @o@׀�� ��O р�� ��O���� @o@ ����  -1�@T�  -1�@T� @o@	��5�      �� 5�      $ Green��6*   5�      5�      5�      �� 5�   
   " d�5�      .R�5�      �� $ Border��6   $ 2,0,0,0��5�      $��0,0,1,0q�5�      ��5�      �� 5�      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5�      � # �  6'   � 5�      # �  6'   $ 1Q�5�      5�      ��5�      �� 5�      $	 0,0&�6*   $	 0,1&�5�      ��5�      � � 5�   "   # �  6/   $	 0.0Q�5�      � 5�   "   # �  6/   $	 1.0Q�5�      5�      5�       5�      
  5�      CheckStates��. 5�        Checked��. 5�      �� .��5�   "   T� $ (UIElement.Visibility)�6@   $ CollapsedArrow��5�   ,   ..�5�   &   i� $ 0��6=   # Z 5�   '   5�   #   T� 5�   "   $ (UIElement.Visibility)�6@   $ ExpandededArrow��5�   ,   ..�5�   &   i� $ 0��6=   # Z 5�   '   5�   #   5�      5�        5�      	Unchecked��6&     5�      Indeterminate��6&   5�      ._�5�      � .+�5�      F� $ CollapsedArrow��6   $��Center=�5�      $��Center=�5�      F����Pq P#� � � �@� Ѱ$� P#� р���� � �  z р�[ ��� �Pq P#� 	��5�      �� 5�      $ DimGray��6.   5�      F� 5�      $ ExpandededArrow��6   $��Center=�5�      $��Center=�5�      $y�	Collapsed=�5�      F����Pq �� � � P�� Ѱ$� �� р���� � �  $� р�[ ��� �Pq �� 	��5�      �� 5�      $ DimGray��6.   5�      5�      5�      5�      �� 5�   
   " 9�6   .�5�      ��  � ��	 5�      �� 5�      " 9�6&   .R�5�      �  5�      �� 5�   "   $
��Auto �60   �� 5�   "   $  
ContentRow��60   $��0 �5�      .+�5�      �� $ Border��6%   $��0��5�   %   
������
���iii�$��1q�5�   %   $ 5,5,0,0��5�   %   ._�5�   "   �  5�   &   �� 5�   *   $��1* �6;   �� 5�   *   $
��auto �6;   .+�5�   &   �� $��0��67   $��4q�6G   $��Header��6R   ��. 6i   d� 5�   &   $��Right=�63   $��1��6O   $��Center=�6_   $��0,0,5,6q�6z   ! . 8��  " 5�   �   �� *
IsExpanded+$# TwoWay=�$ �� *TemplatedParent+5�   '   5�   #   5�      �� 5�      $ Content��6%   $��1��5�      $��1,0,1,1q�5�      $ 	0,0,10,10��5�      ��5�   "   �� 5�   &   # �  66   ��5�   "   �� 5�   &   # � 	 66   ._�5   "   �� $��0,4,0,4q�63   5     5     % 5     P� 5     & �@9�
IsExpanded' . . �5  "   �� $( 
ContentRow��6)   	 5     �� *DesiredHeight+$) Content�� � ��5	     5     5     5     5     .��5     �  5  
   �� 5     $
��auto �6   �� 5     $
��auto �6   �� 5     $
��auto �6   .+�5  
   �� $��0��6   $��5q�6&   .��5     �� $ 6��6   
���iii�$	��1.5q�6<   * �� Padding$* 2q�6R   ._�5     9� -   5        $��	Expander1��5     
���iii�$	��1.5q�5  2   $	��2.5q�6J   $��Dispositivos��5     
���   �
�������+ 9� 
IsExpanded+ 5     �� *ExpanderPanelDevice+$# TwoWay=�.��5      (  "MaterialDesignThemes.Wpf.ColorZone  ,   Mode$
, Dark=�6/   .��5!     �  5"     �� 5#  "   $
��200* �63   �� 5$  "   $
��189* �63   �� 5%  "   $	��11* �63    5'     �� 5(  "   $
��auto �60   �� 5)  "   $
��auto �60   �� 5*  "   $
��auto �60   .+�5-     ]� $��0��6(   $��5��65   .+�50  "   �� $��0,0,10,0q�6)   $��Right=�6;   $	��Top=�51  !   $��24��69   $��24��6D    ,
 (MaterialDesignFloatingActionAccentButton#��[
 6P   
������ 
������ ��50  W   �� *AgregarRadar+.��52  &   (� $��24��6.   $��24��69   .��53  *   �� $��24��61   $��24��6<   .+�54  .   F� |������!@]� �@]� @]� �@]� ��!��ا ��!��ا @]� �@KL @]� �@KL �ا ��ا �ا ��ا @KL �@]� @KL �@]� �ا ���!�ا ���!@]� 	
��� � �6d   55  +   56  '   57  #   58     �� 5<     $��1��6(   $��3��65   $��0,2,0,2q�6I   �� 5>     $��2��6-   $��3��6:   .��5?  "   �� $
��Auto=�6/   $
��Auto=�6R   .��5@  &   &   System.Windows.Controls.DataGrid  $��5q�6/   
���iii�-   AlternatingRowBackground
- �����.   CanUserAddRows. .  /   AutoGenerateColumns/ .  $	��217��5F  '   0   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
0 Auto 5G  '   ��5@  :   �� *RadarDevicesModel_+$# TwoWay=�1 �� SelectedItem1 5A  '   �� *SRadarDevicesModel+2   Columns2 5I  *   0  *System.Windows.Controls.DataGridTextColumn  5J  .   ,  &System.Windows.Controls.DataGridColumn3   Width$3 45 6A   4   Header$4 Id��6L   5   
IsReadOnly5 . 6   FontSize$6 10Q�6�   1  +System.Windows.Controls.DataGridBoundColumn7   Binding7 6X   �� 8 �� Path$8 Id�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5K  .   $3 88 6E   9   CellTemplate9 5L  2   �� 5M  6   .R�5N  :   ]� .+�5O  >   �� ��6F   �� $8 	RadarName�6h   5P  ;   5Q  7   4 5S  2   �� 5T  6   .��5U  :   �� $��Dispositivos��6B   : �� TextWrapping$
: Wrap=�6V   $��0q�6j   $��Center=�6~   $��10(�6�   6�   5V  7   5Y  /     5Z  .   $
3 auto 6E   5 . 9 5[  2   �� 5\  6   .R�5]  :   d� #��[ 6G   $��Center=�5^  r   $��Center=�6�   " 6G   �� $8 StateConnection�5_  ;   5`  7   4 5b  2   �� 5c  6   .��5d  :   �� $��	Conexión��6B   $
: Wrap=�6R   $��0q�6f   $��Center=�6z   $��10(�6�   6�   5e  7   5g  /     5h  .   $3 80 6E   5 . 9 5i  2   �� 5j  6   .R�5k  :   d� #��[ 6G   $��Center=�5l  l   $��Center=�6�   " 6G   �� $8 	Radiation�5m  ;   5n  7   4 5p  2   �� 5q  6   .��5r  :   �� $��
Radiación��6B   $
: Wrap=�6S   $��0q�6g   $��Center=�6{   $��10(�6�   6�   5s  7   5u  /     5v  .   $4 Información��6E   9 5w  2   �� 5x  6   .R�5y  :   �� .+�5z  >   �� 
������   MaterialDesignFlatButton#��[ 5{  ^   ��5z  E   �� $$8 DataContext.DetailRadarCommand�$ �� ; �� AncestorType$; DataGridL�.��5|  B   (� $��24��6J   $��24��6U   .��5}  F   �� $��24��6M   $��24��6X   .+�5~  J   F� ������ا @T� �@]� @T� �@]� ��j ��ا ��j � �  -1�p�s  -1 	= �e�  	=  � � 	= p�s p�s  	=  �  	= Ӑe�  	=  -1p�s  -1 � � -1�e� �e�  -1 �  -1� � �� ׀�  �  ��� ��� � � ��O ��� ��� ׀�O �  ��� ��� � � ��  ��� ��� ��ا @f�@]� @f�@]� �ا ��ا �ا ��ا @f	
������5  O   5�  G   5�  C   5�  ?   5�  ;   5�  7   5�  /   5�  '   5�  #   5�     5�     5�     5�     5�     5�     5�     5�     