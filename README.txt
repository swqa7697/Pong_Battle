# Pong_Battle

------Group Members------
Shuzhan Xie, 59032578, shuzhanx@uci.edu
Kaifan Xu,   55072517, kaifanx@uci.edu

-------Description-------
There are two modes in this Pong game, Player vs. Player and Player vs. AI. The rules for both modes
are the same, and the first one who gets 5 points wins. In addition to the classic Pong game, some
new features were added in. So, besides moving the paddles, players can use abilities with a certain
amount of cooldown time. For instance, players can boost both the ball and the paddle's speed, stick
the ball to the paddle after touching or expand the paddle to better defend the gate. 

We want Pong Battle, as a pong game, to deliver new experiences--more intense and more interesting
than those from the classical pong games--to players. We think when players try to utilize the
abilities during the game for the first a few times, players will have fun in the process of
familiarizing the abilities. In addition, after a few rounds, when players know when to use what 
abilities can be the most efficient, the game can be more interesting and competitive. 

----List of Abilities----
Charge: Accelerate the ball if the player charges the paddle when the ball just hit on the paddle. 
  --Shortcuts: 
    --P1: D
    --P2: ←
  --CD: 0s

Boost: Increase the paddle's speed. 
  --Shortcut: 
    --P1: 4
    --P2: , (Comma)
  --Duration: 5s
  --CD: 15s

Sticky: Stick the ball on the paddle, and release the ball on the next Charge. (The ball will be 
accelerated greater)
  --Shortcut: 
    --P1: 5
    --P2: . (Period)
  --CD: 30s

Expand: Make the paddle longer
  --Shortcut: 
    --P1: 6
    --P2: / (Slash)
  --Duration: 8s
  --CD: 40s
