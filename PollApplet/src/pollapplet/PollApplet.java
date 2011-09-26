/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/*
 * PollApplet.java
 *
 * Created on 17/08/2011, 5:34:40 PM
 */
package pollapplet;

import com.turningtech.poll.Poll;
import com.turningtech.poll.PollService;
import com.turningtech.poll.Response;
import com.turningtech.poll.ResponseListener;
import com.turningtech.test.Test;
import com.turningtech.test.DeviceWakeup;
import com.turningtech.test.Examination;
import com.turningtech.test.Question;
import com.turningtech.test.TestService;
import com.turningtech.test.ResponseTest;
import com.turningtech.test.ResponseListenerTest;
import com.turningtech.receiver.Receiver;
import com.turningtech.receiver.ReceiverService;
import com.turningtech.receiver.ResponseCardLibrary;
import java.awt.Color;
import java.awt.Component;
import java.awt.Font;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Vector;
import javax.swing.DefaultListModel;
import javax.swing.JOptionPane;
import javax.swing.JTable;
import javax.swing.table.AbstractTableModel;
import javax.swing.table.DefaultTableCellRenderer;
import javax.swing.table.DefaultTableModel;
import org.jfree.data.category.DefaultCategoryDataset;
import pollapplet.PollList.dbPoll;
import pollapplet.QuestionList.dbQuestion;
import pollapplet.Responses;

/**
 *
 * @author s4200943
 */
public class PollApplet extends javax.swing.JApplet {

    private Poll poll;
    private ResponseListModel responseListModel = new ResponseListModel();
    private ReceiverListModel receiverListModel = new ReceiverListModel();
    private DefaultCategoryDataset dataset;
    private String test = "test";
    private Responses dbResponses = new Responses();
    private PollList Polls = new PollList();
    private QuestionList Questions = new QuestionList();
    private AnswerList Answers = new AnswerList();
    private dbPoll selectedPoll;
    private dbQuestion selectedQuestion;
    private String Session;
    private boolean polling = false;
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

                    Polls.loadPolls(6);

                    ResponseCardLibrary.initializeLicense("University of Queensland", "24137BBFEEEA9C7F5D65B2432F10F960");
                    initReceivers();
                    initComponents();
                    initModel();


                    selectedPoll = (dbPoll) JOptionPane.showInputDialog(
                            (Component) Frame,
                            "Choose Poll & Session:",
                            "Select a Poll",
                            JOptionPane.PLAIN_MESSAGE,
                            null,
                            Polls.polls.toArray(),
                            Polls.polls.get(0));

                    pollLbl.setText("POLL: " + selectedPoll.getPollName());
                    Questions.loadQuestions(selectedPoll.getPollId());

                    selectedQuestion = Questions.questions.get(QUESTION);
                    questionText.setText(selectedQuestion.getQuestionText());
                    Answers.loadAnswers(selectedQuestion.getQuestionID());

                    setAnswers();

                }
            });
        } catch (Exception ex) {
            ex.printStackTrace();
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

    private void initReceivers() {
        try {
            receiverListModel.addAll(ReceiverService.findReceivers());
        } catch (Exception e) {
            showError("Could not initialize allReceivers.", e);
        }
        List<Receiver> allReceivers = receiverListModel.getReceivers();
        Receiver currentReceiver = allReceivers.get(0);
        currentReceiver.setChannel(22);

        dataset = createDataset();
    }

    private void showError(String message, Exception e) {
        JOptionPane.showMessageDialog(null, message + "\n Reason:" + e.getMessage());
    }

    private void initModel() {

        //keyPadResponseList.setModel(responseListModel);
        testingInfoLabel.setText("Please set your Clicker Device to channel 22 and  wait for testing to begin.");


        System.out.println("Changed!!!!");
        System.out.println(receiverListModel.getSize());

        //responseTable.setModel(responseTableModel);
        //responseTable.setDefaultRenderer(responseTable.getColumnClass(0), PollRenderer);
        //responseTable.setRowHeight(20);

        //receiverList.setModel(receiverListModel);

        /*receiverList.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
        
        public void valueChanged(ListSelectionEvent e) {
        if (e.getValueIsAdjusting()) {
        return;
        }
        int selection = receiverList.getSelectedIndex();
        try {
        Receiver receiver = receiverListModel.get(selection);
        lblChannel.setText(Integer.toString(receiver.getChannel()));
        lblDescription.setText(receiver.getDescription());
        lblId.setText(receiver.getId());
        lblId1.setText(receiver.getVersion());
        } catch (Exception ex) {
        //ignore
        }
        
        }
        });
        receiverList.setCellRenderer(receiverCellRenderer);*/
    }

    private void startPollHandler(java.awt.event.ActionEvent evt) {
        //this.responseChart.setDataset((dataset = createDataset()));
        this.responseListModel.clear();
        System.out.println("Polling for " + selectedQuestion.getQuestionText() + " Started");
        try {//System.out.println("evt paramString: " + evt.getActionCommand());
            //see if it's an invalid response poll that we're starting
            if (evt.getActionCommand().equals("InvalidResponse")) {
                String answerRange = "246809";
                poll = PollService.createCorrectPoll(evt.getActionCommand(), answerRange);
            } else {
                poll = PollService.createPoll();
            }
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
            
            if(responses.contains(newData) && selectedQuestion.getQuestionType() != 6){
                responses.remove(newData);
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
        detectedInfoLabel = new javax.swing.JLabel();
        pollLbl = new javax.swing.JLabel();
        jPanel2 = new javax.swing.JPanel();
        questionText = new javax.swing.JLabel();
        answer4Text = new javax.swing.JLabel();
        answer3Text = new javax.swing.JLabel();
        answer5Text = new javax.swing.JLabel();
        answer2Text = new javax.swing.JLabel();
        answer1Text = new javax.swing.JLabel();
        answer8Text = new javax.swing.JLabel();
        answer7Text = new javax.swing.JLabel();
        answer6Text = new javax.swing.JLabel();
        answer10Text = new javax.swing.JLabel();
        answer9Text = new javax.swing.JLabel();
        prevQuestion = new javax.swing.JButton();
        detectedLbl = new javax.swing.JLabel();
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

        nextQuestion.setFont(new java.awt.Font("Tahoma", 0, 36)); // NOI18N
        nextQuestion.setText("Next Question");
        nextQuestion.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                nextQuestionActionPerformed(evt);
            }
        });

        startButton.setFont(new java.awt.Font("Tahoma", 0, 36)); // NOI18N
        startButton.setText("Start Polling");
        startButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                startButtonActionPerformed(evt);
            }
        });

        detectedInfoLabel.setText("Devices Found");

        pollLbl.setFont(new java.awt.Font("Tahoma", 0, 24));
        pollLbl.setText("poll Text");

        jPanel2.setBackground(new java.awt.Color(255, 255, 255));
        jPanel2.setBorder(javax.swing.BorderFactory.createBevelBorder(javax.swing.border.BevelBorder.LOWERED));
        jPanel2.setMaximumSize(new java.awt.Dimension(760, 396));
        jPanel2.setMinimumSize(new java.awt.Dimension(760, 396));

        questionText.setFont(new java.awt.Font("Tahoma", 0, 36));
        questionText.setText("Question text");
        questionText.setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));
        questionText.setHorizontalTextPosition(javax.swing.SwingConstants.LEFT);

        answer4Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer4Text.setText("1. Answer");

        answer3Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer3Text.setText("1. Answer");

        answer5Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer5Text.setText("1. Answer");

        answer2Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer2Text.setText("1. Answer");

        answer1Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer1Text.setText("1. Answer");

        answer8Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer8Text.setText("1. Answer");

        answer7Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer7Text.setText("1. Answer");

        answer6Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer6Text.setText("1. Answer");

        answer10Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer10Text.setText("1. Answer");

        answer9Text.setFont(new java.awt.Font("Tahoma", 0, 24));
        answer9Text.setText("1. Answer");

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(questionText, javax.swing.GroupLayout.DEFAULT_SIZE, 727, Short.MAX_VALUE)
                        .addContainerGap())
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(answer2Text)
                            .addComponent(answer1Text)
                            .addComponent(answer3Text)
                            .addComponent(answer4Text)
                            .addComponent(answer5Text))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 287, Short.MAX_VALUE)
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(answer10Text)
                            .addComponent(answer6Text)
                            .addComponent(answer8Text)
                            .addComponent(answer7Text)
                            .addComponent(answer9Text))
                        .addGap(238, 238, 238))))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(questionText, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGap(18, 18, 18)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(answer9Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer7Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer8Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer6Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer10Text))
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(answer1Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer2Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer3Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer4Text)
                        .addGap(18, 18, 18)
                        .addComponent(answer5Text)))
                .addContainerGap(88, Short.MAX_VALUE))
        );

        prevQuestion.setFont(new java.awt.Font("Tahoma", 0, 36)); // NOI18N
        prevQuestion.setText("Prev Question");
        prevQuestion.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                prevQuestionActionPerformed(evt);
            }
        });

        detectedLbl.setFont(new java.awt.Font("Tahoma", 0, 24)); // NOI18N
        detectedLbl.setHorizontalAlignment(javax.swing.SwingConstants.TRAILING);
        detectedLbl.setText("0");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(pollLbl)
                .addGap(260, 260, 260)
                .addComponent(detectedLbl, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(detectedInfoLabel)
                .addGap(327, 327, 327))
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                    .addComponent(jPanel2, javax.swing.GroupLayout.Alignment.LEADING, 0, 751, Short.MAX_VALUE)
                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, jPanel1Layout.createSequentialGroup()
                        .addComponent(prevQuestion)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(startButton)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(nextQuestion)))
                .addContainerGap(35, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addComponent(pollLbl)
                        .addGap(7, 7, 7))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(detectedLbl)
                            .addComponent(detectedInfoLabel))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, 382, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(prevQuestion)
                    .addComponent(startButton)
                    .addComponent(nextQuestion))
                .addContainerGap())
        );

        testingInfoLabel.setFont(new java.awt.Font("Tahoma", 0, 14)); // NOI18N
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
                .addContainerGap(53, Short.MAX_VALUE))
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, MasterPanelLayout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, 781, javax.swing.GroupLayout.PREFERRED_SIZE)
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
                .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, 515, Short.MAX_VALUE)
                .addContainerGap())
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(MasterPanel, javax.swing.GroupLayout.DEFAULT_SIZE, 801, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(MasterPanel, javax.swing.GroupLayout.PREFERRED_SIZE, 582, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
    }// </editor-fold>//GEN-END:initComponents

private void nextQuestionActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_nextQuestionActionPerformed

    if ((QUESTION + 1) >= Questions.questions.size()) {
    } else {
        QUESTION++;
        selectedQuestion = Questions.questions.get(QUESTION);
        questionText.setText(selectedQuestion.getQuestionText());
        Answers.loadAnswers(selectedQuestion.getQuestionID());
        setAnswers();
    }



}//GEN-LAST:event_nextQuestionActionPerformed

private void startButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_startButtonActionPerformed
// TODO add your handling code here:

    if (polling == false) {
        startButton.setText("Stop Polling");
        startPollHandler(evt);
        nextQuestion.setEnabled(false);
        prevQuestion.setEnabled(false);
        
        
        
        polling = true;
    } else {
        startButton.setText("Start Polling");
        try {
            poll.stop();
            testingInfoLabel.setText("Testing has finished. Please wait for instruction");
            nextQuestion.setEnabled(true);
            prevQuestion.setEnabled(true);
            dbResponses.saveResponses(responseListModel.responses, selectedPoll.getSessionId(), selectedQuestion.getQuestionID());
            //responseChart.setSubtitle("Polling Closed");
        } catch (Exception e) {
            showError("Unable to stop poll.", e);
        }
        polling = false;
    }

}//GEN-LAST:event_startButtonActionPerformed

private void prevQuestionActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_prevQuestionActionPerformed
// TODO add your handling code here:
    if (QUESTION <= 0) {
    } else {
        QUESTION--;
        selectedQuestion = Questions.questions.get(QUESTION);
        questionText.setText(selectedQuestion.getQuestionText());
        Answers.loadAnswers(selectedQuestion.getQuestionID());
        setAnswers();
    }

}//GEN-LAST:event_prevQuestionActionPerformed
    // Variables declaration - do not modify//GEN-BEGIN:variables
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
    private javax.swing.JLabel detectedInfoLabel;
    private javax.swing.JLabel detectedLbl;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JTextField masterIDTxt;
    private javax.swing.JButton nextQuestion;
    private javax.swing.JDialog pollDialog;
    private javax.swing.JLabel pollLbl;
    private javax.swing.JButton prevQuestion;
    private javax.swing.JLabel questionText;
    private javax.swing.JButton startButton;
    private javax.swing.JButton submitBtn;
    private javax.swing.JLabel testingInfoLabel;
    private javax.swing.JLabel titleLabel;
    private javax.swing.JDialog userDialog;
    // End of variables declaration//GEN-END:variables
}
