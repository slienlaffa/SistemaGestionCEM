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

/**
 * FXML Controller class
 *
 * @author felipe
 */
public class InicioAdministradorController implements Initializable, ControlledScreen {

    ScreensController myController;   
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    
    
    @Override
    public void setScreenParent(ScreensController screenParent) {
        myController = screenParent;
    }
    
    @FXML
    private void handlerAgregarAlumnoAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenAgregarAlumnoID);
    }
    
    @FXML
    private void handlerAgregarCentroAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenAgregarCentroID);
    }
    
    @FXML
    private void handlerAgregarFamiliaAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenAgregarFamiliaID);
    }
    
    @FXML
    private void handlerModificarCentroAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenModificarCentroID);
    }
    
    @FXML
    private void handlerModificarUsuarioAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenModificarUsuarioID);
    }
    
    @FXML
    private void handlerMostrarCentroAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenMostrarCentroID);
    }
    
    @FXML
    private void handlerMostrarUsuarioAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenMostrarUsuarioID);
    }
    
    @FXML
    private void handlerEliminarUsuarioAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenEliminarUsuarioID);
    }
    
    @FXML
    private void handlerEliminarCentroAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenEliminarCentroID);
    }
    
    @FXML
    private void handlerBuscarUsuarioAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenBuscarUsuarioID);
    }
}
