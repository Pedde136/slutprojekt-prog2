   M S B A M L   `   `   `  ����F  BFlappy bird, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Flappy_birdFlappy_bird  5   	       Flappy_bird.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Flappy_bird     5   	     � Title$  Flappy Bird Game��5   	   $	��490��6"   $	��525��6/   .��5	      �� -   5        $��MyCanvas��5	      |�. 
����ح�.+�5   
   �� $	��145��6   $	��200��6   $��Images/clouds.png��6)    �� Left$ 54��6D   
 �� Top$	 152��6U   
 � Tag$ cloud��6f   �� 5   
   $	��145��6   $	��200��6   $��Images/clouds2.png��6)   $	 289��6E   $	 152��6W   $ cloud��6h   �� 5   
   $	��390��6   $��66��6   $��Images/pipeBottom.png��6(   $
 obs1��6G   $ 60��6R   $	 204��6c   �� 5   
   $	��390��6   $��66��6   $��Images/pipeTop.png��6(   $
 obs1��6D   $ 60��6O   $
 -320��6`   �� 5   
   $	��390��6   $��66��6   $��Images/pipeBottom.png��6(   $
 obs2��6G   $	 233��6R   $	 364��6d   �� 5   
   $	��390��6   $��66��6   $��Images/pipeTop.png��6(   $
 obs2��6D   $	 233��6O   $
 -160��6a   �� 5   
   $	��390��6   $��66��6   $��Images/pipeBottom.png��6(   $
 obs3��6G   $	 433��6R   $	 296��6d   �� 5   
   $	��390��6   $��66��6   $��Images/pipeTop.png��6(   $
 obs3��6D   $	 433��6O   $
 -228��6a   �� 5   
   -   5        $��
FlappyBird��5      $��36��6"   $��50��6.   $��images/flappyBird.png��69   $
��Fill=�6X   $ 34��6g   $	 190��6x   �� 5   
   -   5        $��txtScore��5      $��22(�6    $��	ExtraBold"�6.   $��Score: 0��6E   5      5      