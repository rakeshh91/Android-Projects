package com.rakesh.tictoegame;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.GridLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    int activePlayer = 0; //0 = yellow and 1 = red
    int[] gameState = {2, 2, 2, 2, 2, 2, 2, 2, 2};//gamestate = 2 means the slot is not used or tapped
    int[][] ticToePattern = {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}, {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, {0, 4, 8}, {2, 4, 6}};
    boolean activeGame = true;
    public void dropIn(View view) {
        ImageView counter = (ImageView) view;
        int tappedCounter = Integer.parseInt(counter.getTag().toString());
        if (gameState[tappedCounter] == 2 && activeGame) {
            gameState[tappedCounter] = activePlayer;
            counter.setTranslationY(-1000f);
            if (activePlayer == 0) {
                counter.setImageResource(R.drawable.yellow);
                activePlayer = 1;
            } else {
                counter.setImageResource(R.drawable.red);
                activePlayer = 0;
            }
            counter.animate().translationYBy(1000f).rotation(360).setDuration(500);
            for (int[] pattern : ticToePattern) {
                if (gameState[pattern[0]] == gameState[pattern[1]]
                        && gameState[pattern[1]] == gameState[pattern[2]]
                        && gameState[pattern[0]] != 2) {
                    //Active player has won
                    activeGame = false;
                    String colorWinner = "Red player";
                    if (gameState[pattern[0]] == 0) {
                        colorWinner = "Yellow Player";
                    }
                    TextView result = (TextView) findViewById(R.id.resultTextView);
                    result.setText(colorWinner + " has won! Congratulations");

                    LinearLayout playAgainLayout = (LinearLayout) findViewById(R.id.playAgain);
                    playAgainLayout.setVisibility(View.VISIBLE);
                }
                else{
                    boolean drawGame = true;
                    for(int i:gameState){
                        if(i==2){
                            drawGame = false;
                        }
                    }
                    if(drawGame){
                        TextView result = (TextView) findViewById(R.id.resultTextView);
                        result.setText("It is a draw! Play Again");

                        LinearLayout playAgainLayout = (LinearLayout) findViewById(R.id.playAgain);
                        playAgainLayout.setVisibility(View.VISIBLE);

                    }

                }
            }
        }
    }

    public void playGameAgain(View view) {
        activeGame = true;
        LinearLayout playAgainLayout = (LinearLayout) findViewById(R.id.playAgain);
        playAgainLayout.setVisibility(View.INVISIBLE);
        activePlayer = 0;
        for (int i = 0; i < gameState.length; i++) {
            gameState[i] = 2;
        }
        GridLayout gridLayout = (GridLayout) findViewById(R.id.gridLayout);
        for (int i = 0; i < gridLayout.getChildCount(); i++) {
            ((ImageView) gridLayout.getChildAt(i)).setImageResource(0);
        }
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
