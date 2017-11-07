/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sistemagestioncemd;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;


/**
 *
 * @author felipe
 */
public class LoginController implements Initializable, ControlledScreen {
    
    ScreensController myController;
    
    @FXML
    private TextField txtUsuario;
    @FXML
    private PasswordField txtContrasena;
    @FXML
    private Button btnIniciar;
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    

    @Override
    public void setScreenParent(ScreensController screenParent) {
        myController = screenParent;
    }
    
    @FXML
    private void handleIniciarAction(ActionEvent event) {
        
        if(txtUsuario.getText().length() > 0 & txtUsuario.getText().equals("prueba")
                & txtContrasena.getText().length() > 0 & txtContrasena.getText().equals("prueba"))
        {
            System.out.println("Bienvenido");
            myController.setScreen(SistemaGestionCEMD.screenInicioAdministradorID);  
        }
        else
        {
            txtUsuario.clear();
            txtContrasena.clear();
            System.out.println("Equivocado");
        }  
    }
    
}
