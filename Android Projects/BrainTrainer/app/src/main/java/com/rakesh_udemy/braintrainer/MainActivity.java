package com.rakesh_udemy.braintrainer;

import android.graphics.Color;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.w3c.dom.Text;

import java.util.ArrayList;
import java.util.Random;

public class MainActivity extends AppCompatActivity {

    Button playButton;
    Button button0;
    Button button1;
    Button button2;
    Button button3;
    TextView questionTextView;
    TextView introTextView;
    ArrayList<Integer> answers = new ArrayList<Integer>();
    int posofCorrectAnswer;
    int points = 0;
    TextView resultTextView;
    TextView pointsTextView;
    int noOfQuestions;
    TextView timerTextView;
    Button playAgainButton;
    RelativeLayout gameRelativeLayout;


    public void playAgain(View view) {
        points = 0;
        noOfQuestions = 0;
        timerTextView.setText("30s");
        pointsTextView.setText("0/0");
        resultTextView.setText("");
        resultTextView.setTextSize(40f);
        playAgainButton.setVisibility(View.INVISIBLE);

        generateQuestion();
        new CountDownTimer(5100, 1000) {

            @Override
            public void onTick(long millisUntilFinished) {
                timerTextView.setText(String.valueOf(millisUntilFinished / 1000) + "s");
            }

            @Override
            public void onFinish() {
                playAgainButton.setVisibility(View.VISIBLE);
                timerTextView.setText("0s");

                resultTextView.setTextSize(20f);
                resultTextView.setBackgroundColor(Color.parseColor("#afecaf"));
                resultTextView.setTextColor(Color.parseColor("#156002"));
                resultTextView.setText("Your Score is: " + Integer.toString(points) + "/" + Integer.toString(noOfQuestions));
            }
        }.start();


    }

    public void generateQuestion() {

        Random rand = new Random();
        int a = rand.nextInt(21);
        int b = rand.nextInt(21);
        questionTextView.setText(Integer.toString(a) + " + " + Integer.toString(b));
        posofCorrectAnswer = rand.nextInt(4);
        answers.clear();
        int wrongAnswer;

        for (int i = 0; i < 4; i++) {
            if (i == posofCorrectAnswer) {
                answers.add(a + b);
            } else {
                wrongAnswer = rand.nextInt(41);
                while (wrongAnswer == a + b) {
                    wrongAnswer = rand.nextInt(41);
                }
                answers.add(wrongAnswer);
            }
        }
        button0.setText(Integer.toString(answers.get(0)));
        button1.setText(Integer.toString(answers.get(1)));
        button2.setText(Integer.toString(answers.get(2)));
        button3.setText(Integer.toString(answers.get(3)));
    }

    public void chooseAnswer(View view) {

        if (view.getTag().toString().equals(Integer.toString(posofCorrectAnswer))) {
            points++;
            resultTextView.setText("Correct!");
            resultTextView.setBackgroundColor(Color.parseColor("#afecaf"));
            resultTextView.setTextColor(Color.parseColor("#156002"));
        } else {
            resultTextView.setText("Wrong!");
            resultTextView.setBackgroundColor(Color.parseColor("#FFF59489"));
            resultTextView.setTextColor(Color.parseColor("#FF901005"));
        }
        noOfQuestions++;
        pointsTextView.setText(Integer.toString(points) + "/" + Integer.toString(noOfQuestions));
        generateQuestion();
    }

    public void playGame(View view) {
        playButton.setVisibility(View.INVISIBLE);
        introTextView.setVisibility(View.INVISIBLE);
        gameRelativeLayout.setVisibility(View.VISIBLE);
        playAgain(findViewById(R.id.playAgainButton));
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        playButton = (Button) findViewById(R.id.playButton);
        introTextView = (TextView) findViewById(R.id.introTextView);
        questionTextView = (TextView) findViewById(R.id.questionTextView);
        button0 = (Button) findViewById(R.id.button0);
        button1 = (Button) findViewById(R.id.button1);
        button2 = (Button) findViewById(R.id.button2);
        button3 = (Button) findViewById(R.id.button3);
        resultTextView = (TextView) findViewById(R.id.resultTextView);
        pointsTextView = (TextView) findViewById(R.id.pointsTextView);
        timerTextView = (TextView) findViewById(R.id.timerTextView);
        playAgainButton = (Button) findViewById(R.id.playAgainButton);
        gameRelativeLayout = (RelativeLayout) findViewById(R.id.gameRelativeLayout);

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
