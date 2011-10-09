/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 * PollApplet.java
 *
 * Created on 17/08/2011, 5:34:40 PM
 * 
 */
package pollapplet;

import com.turningtech.poll.Poll;
import com.turningtech.poll.PollService;
import com.turningtech.poll.Response;
import com.turningtech.poll.ResponseListener;
import com.turningtech.receiver.Receiver;
import com.turningtech.receiver.ReceiverService;
import com.turningtech.receiver.ResponseCardLibrary;
import java.awt.Component;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import javax.swing.DefaultListModel;
import javax.swing.JOptionPane;
import javax.swing.WindowConstants;
import org.jfree.data.category.CategoryDataset;
import org.jfree.data.category.DefaultCategoryDataset;
import org.jfree.ui.RefineryUtilities;
import pollapplet.PollList.dbPoll;
import pollapplet.QuestionList.dbQuestion;

/**
 *
 * @author 42009432 - Adam Young
 */
public class PollApplet extends javax.swing.JApplet {

    private LinkedList<String> TOTALRESPONSES = new LinkedList<String>();
    private LinkedList<String> CURRENTRESPONSES = new LinkedList<String>();
    private Poll poll;
    private ResponseListModel responseListModel = new ResponseListModel();
    private ReceiverListModel receiverListModel = new ReceiverListModel();
    private Responses dbResponses = new Responses();
    private PollList Polls = new PollList();
    private QuestionList Questions = new QuestionList();
    private AnswerList Answers = new AnswerList();
    private dbPoll selectedPoll;
    private dbQuestion selectedQuestion;
    private boolean polling = false;
    private DefaultCategoryDataset dataset;
    int testQuestion = 0;
    /**
     * Current Question during polling. (Location in question list)
     */
    private int QUESTION = 0;

    /** Initializes the applet PollApplet */
    @Override
    public void init() {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Windows".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(PollApplet.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the applet */
        try {
            java.awt.EventQueue.invokeAndWait(new Runnable() {

                @Override
                public void run() {
                    try {


                        if (getParameter("quest_id") != null) {
                            testQuestion = Integer.parseInt(getParameter("quest_id"));
                        }
                        //testQuestion = 1;
                        //Polls.loadPolls(10);

                        ResponseCardLibrary.initializeLicense("University of Queensland", "24137BBFEEEA9C7F5D65B2432F10F960");
                        initReceivers();
                        initComponents();
                        initModel();

                        if (testQuestion == 0) {
                            Polls.loadPolls(Integer.parseInt(getParameter("poll_master")));

                            while (selectedPoll == null) {
                                selectedPoll = (dbPoll) JOptionPane.showInputDialog(
                                        (Component) Frame,
                                        "Choose Poll & Session:",
                                        "Select a Poll",
                                        JOptionPane.PLAIN_MESSAGE,
                                        null,
                                        Polls.polls.toArray(),
                                        Polls.polls.get(0));
                                if (selectedPoll == null) {
                                    JOptionPane.showMessageDialog(null, "You must select a poll!", "Poll Error", 1);
                                }
                            }

                            pollLbl.setText("POLL: " + selectedPoll.getPollName());

                            Questions.loadQuestions(selectedPoll.getPollId());
                        } else {

                            pollLbl.setText("Dummy Poll: Testing Question");
                            Questions.loadTestQuestion(testQuestion);

                        }

                        selectedQuestion = Questions.questions.get(QUESTION);
                        questionText.setText(selectedQuestion.getQuestionText());
                        Answers.loadAnswers(selectedQuestion.getQuestionID());

                    } catch (SQLException ex) {
                        showError("Fatal Error: Could not Connect to database. F5 to continue.", ex);
                    }
                    setAnswers();
                    if (testQuestion != 0) {
                        nextQuestion.setEnabled(false);
                        prevQuestion.setEnabled(false);
                    }
                }
            });
        } catch (Exception ex) {
            showError("Fatal Error: Could not Initialise Applet. F5 to continue.", ex);
        }
    }

    private void setAnswers() {
        answer1Text.setText(((Answers.answers.size() >= 1) ? "1. " + Answers.answers.get(0).getAnswerText() : " "));
        answer2Text.setText(((Answers.answers.size() >= 2) ? "2. " + Answers.answers.get(1).getAnswerText() : " "));
        answer3Text.setText(((Answers.answers.size() >= 3) ? "3. " + Answers.answers.get(2).getAnswerText() : " "));
        answer4Text.setText(((Answers.answers.size() >= 4) ? "4. " + Answers.answers.get(3).getAnswerText() : " "));
        answer5Text.setText(((Answers.answers.size() >= 5) ? "5. " + Answers.answers.get(4).getAnswerText() : " "));
        answer6Text.setText(((Answers.answers.size() >= 6) ? "6. " + Answers.answers.get(5).getAnswerText() : " "));
        answer7Text.setText(((Answers.answers.size() >= 7) ? "7. " + Answers.answers.get(6).getAnswerText() : " "));
        answer8Text.setText(((Answers.answers.size() >= 8) ? "8. " + Answers.answers.get(7).getAnswerText() : " "));
        answer9Text.setText(((Answers.answers.size() >= 9) ? "9. " + Answers.answers.get(8).getAnswerText() : " "));
        answer10Text.setText(((Answers.answers.size() >= 10) ? "10. " + Answers.answers.get(9).getAnswerText() : ""));
    }

    public void createTestResponseSet() {
        responseListModel.add(new Response("00A99A", "1", "1"));
        responseListModel.add(new Response("00A99A", "2", "2"));
        responseListModel.add(new Response("00A99A", "3", "3"));
        responseListModel.add(new Response("00A99A", "4", "3"));
        responseListModel.add(new Response("00A99A", "5", "3"));
        responseListModel.add(new Response("00A99A", "6", "1"));
        responseListModel.add(new Response("00A99A", "7", "2"));
        responseListModel.add(new Response("00A99A", "8", "2"));
        responseListModel.add(new Response("00A99A", "9", "1"));
        responseListModel.add(new Response("00A99A", "10", "1"));
        responseListModel.add(new Response("00A99A", "11", "1"));

    }

    private void initReceivers() {
        try {
            receiverListModel.addAll(ReceiverService.findReceivers());

            List<Receiver> allReceivers = receiverListModel.getReceivers();
            Receiver currentReceiver = allReceivers.get(0);
            currentReceiver.setChannel(22);

            dataset = createDataset();
        } catch (Exception e) {
            showError("Could not initialize allReceivers.", e);
        }
    }

    private void showError(String message, Exception e) {
        JOptionPane.showMessageDialog(null, message + "\n Reason:" + e.getMessage());
        System.exit(-1);
    }

    private void initModel() {

        //keyPadResponseList.setModel(responseListModel);
        testingInfoLabel.setText("Please set your Clicker Device to channel 22 and  wait for testing to begin.");

        System.out.println("Changed!!!!");
        System.out.println(receiverListModel.getSize());

    }

    private void startPollHandler(java.awt.event.ActionEvent evt) {
        //this.responseChart.setDataset((dataset = createDataset()));
        this.responseListModel.clear();
        System.out.println("Polling for " + selectedQuestion.getQuestionText() + " Started");
        try {//System.out.println("evt paramString: " + evt.getActionCommand());
            //see if it's an invalid response poll that we're starting

            String answerRange = "123";
            poll = PollService.createCorrectPoll("InvalidResponse", answerRange);
            //poll = PollService.createPoll();

            poll.addResponseListener(new BasicResponseListener());
            Poll.PollingMode pollingMode;

            // Set Answering type depending on question type from database.
            if (selectedQuestion.getQuestionType() > 2 && selectedQuestion.getQuestionType() < 6) {
                pollingMode = (Poll.PollingMode.SingleResponse_Numeric);
            } else if (selectedQuestion.getQuestionType() == 6) {
                pollingMode = (Poll.PollingMode.MultiResponse_Numeric);
            } else if (selectedQuestion.getQuestionType() == 1) {
                pollingMode = (Poll.PollingMode.Numeric);
            } else if (selectedQuestion.getQuestionType() == 2) {
                pollingMode = (Poll.PollingMode.ShortAnswer);
            } else {
                pollingMode = (Poll.PollingMode.SingleResponse_Numeric);
            }
            poll.start(pollingMode);
            testingInfoLabel.setText("Please press Keys on your Clicker Device to ensure your responses are received");


            //responseChart.setSubtitle(evt.getActionCommand() + " Polling Open");
        } catch (Exception e) {
            showError("Unable to start poll.", e);
        }
    }

    private class ReceiverListModel extends DefaultListModel {

        private List<Receiver> receivers = new ArrayList();

        @Override
        public void clear() {
            receivers.clear();
            fireContentsChanged(this, 0, 0);
        }

        public void addAll(Collection newData) {
            receivers.addAll(newData);
            fireContentsChanged(this, 0, 0);
        }

        @Override
        public int getSize() {
            return receivers.size();
        }

        @Override
        public Object getElementAt(int index) {
            return receivers.get(index);
        }

        @Override
        public Receiver get(int index) {
            return receivers.get(index);
        }

        public List<Receiver> getReceivers() {
            return receivers;
        }
    }

    public class ResponseListModel extends DefaultListModel {

        public List<Response> responses = new ArrayList();

        @Override
        public void clear() {
            responses.clear();
            fireContentsChanged(this, 0, 0);
        }

        public void add(Response newData) {

            if (selectedQuestion.getQuestionType() != 6) {
                for (Response resp : responses) {
                    if (resp.getResponseCardId().equals(newData.getResponseCardId())) {
                        responses.remove(resp);
                        break;
                    }
                }
            }

            if (!TOTALRESPONSES.contains(newData.getResponseCardId())) {
                TOTALRESPONSES.add(newData.getResponseCardId());
                detectTotalLbl.setText("" + TOTALRESPONSES.size());
            }

            if (!CURRENTRESPONSES.contains(newData.getResponseCardId())) {
                CURRENTRESPONSES.add(newData.getResponseCardId());
                detectQuestLbl.setText("" + CURRENTRESPONSES.size());
            }


            responses.add(newData);
            System.out.println("device: '" + newData.getResponseCardId() + "' Response: '" + newData.getResponse() + "'");

            fireContentsChanged(this, getSize() - 2, getSize() - 1);
        }

        @Override
        public int getSize() {
            return responses.size();
        }

        @Override
        public Object getElementAt(int index) {
            return responses.get(index);
        }
    }

    private DefaultCategoryDataset createDataset() {
        DefaultCategoryDataset newDataset = new DefaultCategoryDataset();
        List<Receiver> receivers = receiverListModel.getReceivers();
        for (Receiver receiver : receivers) {
            for (int i = 0; i < 10; i++) {
                newDataset.addValue(0, receiver.getId(), Integer.toString(i));
            }
        }

        return newDataset;

    }

    private class BasicResponseListener implements ResponseListener {

        public BasicResponseListener() {
        }

        @Override
        public void responseReceived(Response response) {
            responseListModel.add(response);
            if (response.getReceiverId() == null
                    || response.getResponse() == null
                    || response.getResponse().equals("?")) {
                return;
            }

//            if (!dataset.getColumnKeys().contains(response.getResponse()))

            /**
            Number count = dataset.getValue(response.getReceiverId(), response.getResponse());
            if (count == null) {
            count = new Integer(1);
            }
             */
            //dataset.incrementValue(1, response.getReceiverId(), response.getResponse());
        }
    }

    /** This method is called from within the init() method to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        userDialog = new javax.swing.JDialog();
        jLabel2 = new javax.swing.JLabel();
        masterIDTxt = new javax.swing.JTextField();
        submitBtn = new javax.swing.JButton();
        jLabel3 = new javax.swing.JLabel();
        pollDialog = new javax.swing.JDialog();
        Frame = new javax.swing.JFrame();
        MasterPanel = new javax.swing.JPanel();
        titleLabel = new javax.swing.JLabel();
        jPanel1 = new javax.swing.JPanel();
        nextQuestion = new javax.swing.JButton();
        startButton = new javax.swing.JButton();
        pollLbl = new javax.swing.JLabel();
        jPanel2 = new javax.swing.JPanel();
        questionText = new javax.swing.JLabel();
        AnswerPanel1 = new javax.swing.JPanel();
        answer6Text = new javax.swing.JLabel();
        answer10Text = new javax.swing.JLabel();
        answer8Text = new javax.swing.JLabel();
        answer7Text = new javax.swing.JLabel();
        answer9Text = new javax.swing.JLabel();
        AnswerPanel2 = new javax.swing.JPanel();
        answer1Text = new javax.swing.JLabel();
        answer2Text = new javax.swing.JLabel();
        answer3Text = new javax.swing.JLabel();
        answer4Text = new javax.swing.JLabel();
        answer5Text = new javax.swing.JLabel();
        prevQuestion = new javax.swing.JButton();
        jPanel3 = new javax.swing.JPanel();
        detectQuestLbl = new javax.swing.JLabel();
        slash = new javax.swing.JLabel();
        detectedInfoLabel = new javax.swing.JLabel();
        detectTotalLbl = new javax.swing.JLabel();
        testingInfoLabel = new javax.swing.JLabel();

        jLabel2.setText("Poll Master Id = ");

        submitBtn.setText("Submit");

        jLabel3.setFont(new java.awt.Font("Tahoma", 0, 18));
        jLabel3.setText("<Used For Testing> ");

        javax.swing.GroupLayout userDialogLayout = new javax.swing.GroupLayout(userDialog.getContentPane());
        userDialog.getContentPane().setLayout(userDialogLayout);
        userDialogLayout.setHorizontalGroup(
            userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(userDialogLayout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(userDialogLayout.createSequentialGroup()
                        .addComponent(jLabel2)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addComponent(submitBtn, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(masterIDTxt, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.PREFERRED_SIZE, 77, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addComponent(jLabel3)))
        );
        userDialogLayout.setVerticalGroup(
            userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(userDialogLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel3)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(userDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel2)
                    .addComponent(masterIDTxt, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(submitBtn)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout pollDialogLayout = new javax.swing.GroupLayout(pollDialog.getContentPane());
        pollDialog.getContentPane().setLayout(pollDialogLayout);
        pollDialogLayout.setHorizontalGroup(
            pollDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 400, Short.MAX_VALUE)
        );
        pollDialogLayout.setVerticalGroup(
            pollDialogLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 300, Short.MAX_VALUE)
        );

        javax.swing.GroupLayout FrameLayout = new javax.swing.GroupLayout(Frame.getContentPane());
        Frame.getContentPane().setLayout(FrameLayout);
        FrameLayout.setHorizontalGroup(
            FrameLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 400, Short.MAX_VALUE)
        );
        FrameLayout.setVerticalGroup(
            FrameLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 300, Short.MAX_VALUE)
        );

        MasterPanel.setMaximumSize(new java.awt.Dimension(800, 600));
        MasterPanel.setPreferredSize(new java.awt.Dimension(800, 600));

        titleLabel.setFont(new java.awt.Font("Tahoma", 0, 28));
        titleLabel.setText("KeyPad Polling");

        jPanel1.setBorder(new javax.swing.border.LineBorder(new java.awt.Color(0, 0, 0), 1, true));
        jPanel1.setMaximumSize(new java.awt.Dimension(800, 600));
        jPanel1.setPreferredSize(new java.awt.Dimension(800, 600));

        nextQuestion.setFont(new java.awt.Font("Tahoma", 0, 36));
        nextQuestion.setText("Next Question");
        nextQuestion.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                nextQuestionActionPerformed(evt);
            }
        });

        startButton.setFont(new java.awt.Font("Tahoma", 0, 36));
        startButton.setText("Start Polling");
        startButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                startButtonActionPerformed(evt);
            }
        });

        pollLbl.setFont(new java.awt.Font("Tahoma", 0, 24));
        pollLbl.setText("poll Text");

        jPanel2.setBackground(new java.awt.Color(255, 255, 255));
        jPanel2.setBorder(javax.swing.BorderFactory.createBevelBorder(javax.swing.border.BevelBorder.LOWERED));
        jPanel2.setMaximumSize(new java.awt.Dimension(760, 396));
        jPanel2.setMinimumSize(new java.awt.Dimension(760, 396));
        jPanel2.setPreferredSize(new java.awt.Dimension(600, 396));

        questionText.setFont(new java.awt.Font("Tahoma", 0, 36));
        questionText.setText("Question text");
        questionText.setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));
        questionText.setHorizontalTextPosition(javax.swing.SwingConstants.LEFT);

        AnswerPanel1.setBackground(new java.awt.Color(255, 255, 255));
        AnswerPanel1.setPreferredSize(new java.awt.Dimension(351, 310));

        answer6Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer6Text.setText("1. Answer");

        answer10Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer10Text.setText("1. Answer");

        answer8Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer8Text.setText("1. Answer");

        answer7Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer7Text.setText("1. Answer");

        answer9Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer9Text.setText("1. Answer");

        javax.swing.GroupLayout AnswerPanel1Layout = new javax.swing.GroupLayout(AnswerPanel1);
        AnswerPanel1.setLayout(AnswerPanel1Layout);
        AnswerPanel1Layout.setHorizontalGroup(
            AnswerPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(AnswerPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(AnswerPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(answer7Text)
                    .addComponent(answer9Text)
                    .addComponent(answer8Text)
                    .addComponent(answer6Text)
                    .addComponent(answer10Text))
                .addContainerGap(254, Short.MAX_VALUE))
        );
        AnswerPanel1Layout.setVerticalGroup(
            AnswerPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(AnswerPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(answer7Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(answer9Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(answer8Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 11, Short.MAX_VALUE)
                .addComponent(answer10Text, javax.swing.GroupLayout.PREFERRED_SIZE, 40, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(answer6Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );

        AnswerPanel2.setBackground(new java.awt.Color(255, 255, 255));

        answer1Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer1Text.setText("1. Answer");

        answer2Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer2Text.setText("1. Answer");

        answer3Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer3Text.setText("1. Answer");

        answer4Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer4Text.setText("1. Answer");

        answer5Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer5Text.setText("1. Answer");

        javax.swing.GroupLayout AnswerPanel2Layout = new javax.swing.GroupLayout(AnswerPanel2);
        AnswerPanel2.setLayout(AnswerPanel2Layout);
        AnswerPanel2Layout.setHorizontalGroup(
            AnswerPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(AnswerPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(AnswerPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(answer3Text)
                    .addComponent(answer4Text)
                    .addComponent(answer5Text)
                    .addComponent(answer2Text)
                    .addComponent(answer1Text))
                .addContainerGap(235, Short.MAX_VALUE))
        );
        AnswerPanel2Layout.setVerticalGroup(
            AnswerPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, AnswerPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(answer1Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(answer2Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(answer3Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(answer4Text, javax.swing.GroupLayout.DEFAULT_SIZE, 45, Short.MAX_VALUE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(answer5Text, javax.swing.GroupLayout.PREFERRED_SIZE, 56, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(AnswerPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(AnswerPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, 370, Short.MAX_VALUE)
                        .addContainerGap())
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(questionText, javax.swing.GroupLayout.DEFAULT_SIZE, 366, Short.MAX_VALUE)
                        .addGap(371, 371, 371))))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addComponent(questionText, javax.swing.GroupLayout.PREFERRED_SIZE, 60, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(AnswerPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, 315, Short.MAX_VALUE)
                    .addComponent(AnswerPanel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );

        prevQuestion.setFont(new java.awt.Font("Tahoma", 0, 36));
        prevQuestion.setText("Prev Question");
        prevQuestion.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                prevQuestionActionPerformed(evt);
            }
        });

        detectQuestLbl.setFont(new java.awt.Font("Tahoma", 0, 24));
        detectQuestLbl.setHorizontalAlignment(javax.swing.SwingConstants.TRAILING);
        detectQuestLbl.setText("0");

        slash.setFont(new java.awt.Font("Tahoma", 0, 24));
        slash.setHorizontalAlignment(javax.swing.SwingConstants.TRAILING);
        slash.setText("/");

        detectedInfoLabel.setText("Devices Found");

        detectTotalLbl.setFont(new java.awt.Font("Tahoma", 0, 24));
        detectTotalLbl.setHorizontalAlignment(javax.swing.SwingConstants.TRAILING);
        detectTotalLbl.setText("0");

        javax.swing.GroupLayout jPanel3Layout = new javax.swing.GroupLayout(jPanel3);
        jPanel3.setLayout(jPanel3Layout);
        jPanel3Layout.setHorizontalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addGap(2, 2, 2)
                .addComponent(detectQuestLbl, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(slash)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(detectTotalLbl, javax.swing.GroupLayout.PREFERRED_SIZE, 28, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(detectedInfoLabel)
                .addContainerGap(48, Short.MAX_VALUE))
        );
        jPanel3Layout.setVerticalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel3Layout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(detectQuestLbl)
                    .addComponent(detectedInfoLabel)
                    .addComponent(slash)
                    .addComponent(detectTotalLbl)))
        );

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                        .addComponent(pollLbl)
                        .addGap(183, 183, 183)
                        .addComponent(jPanel3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(516, 516, 516))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addComponent(jPanel2, javax.swing.GroupLayout.Alignment.LEADING, 0, 751, Short.MAX_VALUE)
                            .addGroup(javax.swing.GroupLayout.Alignment.LEADING, jPanel1Layout.createSequentialGroup()
                                .addComponent(prevQuestion)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(startButton)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                                .addComponent(nextQuestion)))
                        .addContainerGap())))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(jPanel3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(pollLbl))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(nextQuestion)
                    .addComponent(startButton)
                    .addComponent(prevQuestion))
                .addContainerGap())
        );

        testingInfoLabel.setFont(new java.awt.Font("Tahoma", 0, 14));
        testingInfoLabel.setText("Please press Keys on your Clicker Device to ensure your responses are received");

        javax.swing.GroupLayout MasterPanelLayout = new javax.swing.GroupLayout(MasterPanel);
        MasterPanel.setLayout(MasterPanelLayout);
        MasterPanelLayout.setHorizontalGroup(
            MasterPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(MasterPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(titleLabel, javax.swing.GroupLayout.PREFERRED_SIZE, 186, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(56, 56, 56)
                .addComponent(testingInfoLabel, javax.swing.GroupLayout.PREFERRED_SIZE, 496, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(52, Short.MAX_VALUE))
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, MasterPanelLayout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, 800, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        MasterPanelLayout.setVerticalGroup(
            MasterPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(MasterPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(MasterPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(testingInfoLabel, javax.swing.GroupLayout.PREFERRED_SIZE, 17, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(titleLabel))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, 535, Short.MAX_VALUE)
                .addContainerGap())
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(MasterPanel, javax.swing.GroupLayout.PREFERRED_SIZE, 800, javax.swing.GroupLayout.PREFERRED_SIZE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(MasterPanel, javax.swing.GroupLayout.PREFERRED_SIZE, 602, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
    }// </editor-fold>//GEN-END:initComponents

    public int checkExists(int response) {
        //if the response exists, return 1 else 0
        int result = ((Answers.answers.size() >= response) ? 1 : 0);

        System.out.println("Result :" + result);
        return result;
    }

    public CategoryDataset createGraphDataset() {

        //createTestResponseSet();
        int answerSize = Answers.answers.size();

        // row keys...
        String series1 = selectedQuestion.getQuestionText();

        // column keys...

        int[] ResponseTotals = new int[10];

        String category1 = ((answerSize >= 1) ? "1. " + Answers.answers.get(0).getAnswerText() : " ");
        String category2 = ((answerSize >= 2) ? "2. " + Answers.answers.get(1).getAnswerText() : " ");
        String category3 = ((answerSize >= 3) ? "3. " + Answers.answers.get(2).getAnswerText() : " ");
        String category4 = ((answerSize >= 4) ? "4. " + Answers.answers.get(3).getAnswerText() : " ");
        String category5 = ((answerSize >= 5) ? "5. " + Answers.answers.get(4).getAnswerText() : " ");
        String category6 = ((answerSize >= 6) ? "6. " + Answers.answers.get(5).getAnswerText() : " ");
        String category7 = ((answerSize >= 7) ? "7. " + Answers.answers.get(6).getAnswerText() : " ");
        String category8 = ((answerSize >= 8) ? "8. " + Answers.answers.get(7).getAnswerText() : " ");
        String category9 = ((answerSize >= 9) ? "9. " + Answers.answers.get(8).getAnswerText() : " ");
        String category10 = ((answerSize >= 10) ? "10. " + Answers.answers.get(9).getAnswerText() : " ");

        for (Response res : responseListModel.responses) {

            //Check if response is valid to count
            int response = Integer.parseInt(res.getResponse());
            if (response <= answerSize) {
                System.out.println("response :" + response);
                //Check what to add it to
                switch (response) {
                    case 1:
                        ResponseTotals[0] += checkExists(response);
                        break;
                    case 2:
                        ResponseTotals[1] += checkExists(response);
                        break;
                    case 3:
                        ResponseTotals[2] += checkExists(response);
                        break;
                    case 4:
                        ResponseTotals[3] += checkExists(response);
                        break;
                    case 5:
                        ResponseTotals[4] += checkExists(response);
                        break;
                    case 6:
                        ResponseTotals[5] += checkExists(response);
                        break;
                    case 7:
                        ResponseTotals[6] += checkExists(response);
                        break;
                    case 8:
                        ResponseTotals[7] += checkExists(response);
                        break;
                    case 9:
                        ResponseTotals[8] += checkExists(response);
                        break;
                    case 10:
                        ResponseTotals[9] += checkExists(response);
                        break;
                    default:
                        ;
                        break;
                }
            }
        }

        // create the dataset...
        final DefaultCategoryDataset graphDataset = new DefaultCategoryDataset();

        if (answerSize >= 1) {
            graphDataset.addValue(ResponseTotals[0], series1, category1);
        }
        if (answerSize >= 2) {
            graphDataset.addValue(ResponseTotals[1], series1, category2);
        }
        if (answerSize >= 3) {
            graphDataset.addValue(ResponseTotals[2], series1, category3);
        }
        if (answerSize >= 4) {
            graphDataset.addValue(ResponseTotals[3], series1, category4);
        }
        if (answerSize >= 5) {
            graphDataset.addValue(ResponseTotals[4], series1, category5);
        }
        if (answerSize >= 6) {
            graphDataset.addValue(ResponseTotals[5], series1, category6);
        }
        if (answerSize >= 7) {
            graphDataset.addValue(ResponseTotals[6], series1, category7);
        }
        if (answerSize >= 8) {
            graphDataset.addValue(ResponseTotals[7], series1, category8);
        }
        if (answerSize >= 9) {
            graphDataset.addValue(ResponseTotals[8], series1, category9);
        }
        if (answerSize >= 10) {
            graphDataset.addValue(ResponseTotals[9], series1, category10);
        }


        return graphDataset;
    }

private void nextQuestionActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_nextQuestionActionPerformed

    if ((QUESTION + 1) >= Questions.questions.size()) {
    } else {

        QUESTION++;
        detectQuestLbl.setText("" + CURRENTRESPONSES.size());
        selectedQuestion = Questions.questions.get(QUESTION);
        try {
            Questions.setNextQuestion(selectedPoll.getSessionId(),
                    selectedQuestion.getQuestionID());
        } catch (SQLException ex) {
            showError("Fatal Error: Unable to update database. "
                    + "Restart application to resume polling", ex);
        }
        questionText.setText(selectedQuestion.getQuestionText());
        try {
            Answers.loadAnswers(selectedQuestion.getQuestionID());
        } catch (SQLException ex) {
            showError("Fatal Error: Unable to update database. "
                    + "Restart application to resume polling", ex);
        }
        setAnswers();
    }

//    Questions.setNextQuestion(selectedPoll.getSessionId(),
    //              0);
}//GEN-LAST:event_nextQuestionActionPerformed

private void startButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_startButtonActionPerformed

    if (polling == false) {
        detectQuestLbl.setText("" + CURRENTRESPONSES.size());
        startButton.setText("Stop Polling");
        startPollHandler(evt);
        try {
            if (testQuestion != 0) {
                Questions.openQuestion(selectedPoll.getSessionId(),
                        selectedQuestion.getQuestionID());

            }
        } catch (SQLException ex) {
            showError("Fatal Error: Unable to update database. "
                    + "Restart application to resume polling", ex);
        }
        if (testQuestion != 0) {
            nextQuestion.setEnabled(false);
            prevQuestion.setEnabled(false);
        }
        polling = true;
    } else {
        
        

        //createTestResponseSet();
        final Chart demo;

        
        if (testQuestion != 0) {
            demo = new Chart("Chart for Poll: " + selectedPoll.getPollName(), selectedQuestion.getQuestionText(), createGraphDataset(), selectedQuestion.getChartStyle());
        } else {
            demo = new Chart("Chart for Poll: " + "Dummy Poll", selectedQuestion.getQuestionText(), createGraphDataset(), selectedQuestion.getChartStyle());
        }
        demo.pack();
        RefineryUtilities.centerFrameOnScreen(demo);
        
        demo.setVisible(true);
        demo.setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);

        startButton.setText("Start Polling");
        try {
            CURRENTRESPONSES = new LinkedList<String>();

            // Close question to polling by applet
            poll.stop();

            // Close question to polling by synchronous web application

            if (testQuestion != 0) {
                Questions.closeQuestion(selectedPoll.getSessionId(),
                        selectedQuestion.getQuestionID());
            }
            testingInfoLabel.setText("Testing has finished. Please wait for instruction");

            if (testQuestion != 0) {
                nextQuestion.setEnabled(true);
                prevQuestion.setEnabled(true);
            }
            if (testQuestion != 0) {
                dbResponses.saveResponses(responseListModel.responses, selectedPoll.getSessionId(), selectedQuestion.getQuestionID());
            }
            responseListModel = new ResponseListModel();
            //responseChart.setSubtitle("Polling Closed");
        } catch (SQLException ex) {
            showError("Fatal Error: Unable to update database. "
                    + "Restart application to resume polling", ex);
        } catch (Exception e) {
            showError("Unable to stop poll.", e);
        }
        polling = false;
    }
}//GEN-LAST:event_startButtonActionPerformed

private void prevQuestionActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_prevQuestionActionPerformed

    if (QUESTION <= 0) {
    } else {
        QUESTION--;
        selectedQuestion = Questions.questions.get(QUESTION);
        try {
            Questions.setNextQuestion(selectedPoll.getSessionId(),
                    selectedQuestion.getQuestionID());
        } catch (SQLException ex) {
            showError("Fatal Error: Unable to update database. "
                    + "Restart application to resume polling", ex);
        }

        detectQuestLbl.setText("" + CURRENTRESPONSES.size());
        questionText.setText(selectedQuestion.getQuestionText());
        try {
            Answers.loadAnswers(selectedQuestion.getQuestionID());
        } catch (SQLException ex) {
            showError("Fatal Error: Unable to update database. "
                    + "Restart application to resume polling", ex);
        }
        setAnswers();
    }



}//GEN-LAST:event_prevQuestionActionPerformed
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JPanel AnswerPanel1;
    private javax.swing.JPanel AnswerPanel2;
    private javax.swing.JFrame Frame;
    private javax.swing.JPanel MasterPanel;
    private javax.swing.JLabel answer10Text;
    private javax.swing.JLabel answer1Text;
    private javax.swing.JLabel answer2Text;
    private javax.swing.JLabel answer3Text;
    private javax.swing.JLabel answer4Text;
    private javax.swing.JLabel answer5Text;
    private javax.swing.JLabel answer6Text;
    private javax.swing.JLabel answer7Text;
    private javax.swing.JLabel answer8Text;
    private javax.swing.JLabel answer9Text;
    private javax.swing.JLabel detectQuestLbl;
    private javax.swing.JLabel detectTotalLbl;
    private javax.swing.JLabel detectedInfoLabel;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel jPanel3;
    private javax.swing.JTextField masterIDTxt;
    private javax.swing.JButton nextQuestion;
    private javax.swing.JDialog pollDialog;
    private javax.swing.JLabel pollLbl;
    private javax.swing.JButton prevQuestion;
    private javax.swing.JLabel questionText;
    private javax.swing.JLabel slash;
    private javax.swing.JButton startButton;
    private javax.swing.JButton submitBtn;
    private javax.swing.JLabel testingInfoLabel;
    private javax.swing.JLabel titleLabel;
    private javax.swing.JDialog userDialog;
    // End of variables declaration//GEN-END:variables
}
