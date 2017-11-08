/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sistemagestioncemd;

import java.net.URL;
import java.time.LocalDate;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.ComboBox;
import javafx.scene.control.DatePicker;
import javafx.scene.control.TextField;

/**
 * FXML Controller class
 *
 * @author felipe
 */
public class AlumnoController implements Initializable, ControlledScreen {

    ScreensController myController;
    
    @FXML
    TextField txtNombre;
    @FXML
    TextField txtApellidoPaterno;
    @FXML
    TextField txtApellidoMaterno;
    @FXML
    TextField txtDireccion;
    @FXML
    TextField txtNContacto;
    @FXML
    TextField txtCorreo;
    @FXML
    ComboBox cbxGenero;
    @FXML
    ComboBox cbxPais;
    @FXML
    ComboBox cbxCiudad;
    @FXML
    DatePicker dpFechaNacimiento;
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }

    @Override
    public void setScreenParent(ScreensController screenParent) {
        myController = screenParent;
    }
    
    @FXML
    private void handlerGuardarAlumnoAction(ActionEvent event) {
        if(true){
            //aqui cosas de guardar
        
            myController.setScreen(SistemaGestionCEMD.screenInicioAdministradorID);
        } else{
            //accion de si no guarda
        }      
    }
    
    @FXML
    private void handlerLimpiarAgregarAlumnoAction(ActionEvent event) {
        txtNombre.clear();
        txtApellidoMaterno.clear();
        txtApellidoPaterno.clear();
        txtCorreo.clear();
        txtDireccion.clear();
        txtNContacto.clear();
        cbxCiudad.getSelectionModel().clearSelection();
        cbxGenero.getSelectionModel().clearSelection();
        cbxPais.getSelectionModel().clearSelection();
        dpFechaNacimiento.setValue(LocalDate.MAX);
    }
    
    @FXML
    private void handlerVolverAction(ActionEvent event) {
        myController.setScreen(SistemaGestionCEMD.screenInicioAdministradorID);
    }
}
