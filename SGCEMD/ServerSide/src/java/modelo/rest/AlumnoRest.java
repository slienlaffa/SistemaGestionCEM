/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package modelo.rest;

import java.util.List;
import javax.inject.Inject;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.UriInfo;
import javax.ws.rs.Produces;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PUT;
import javax.ws.rs.PathParam;
import javax.ws.rs.core.MediaType;
import modelo.entity.Alumno;

/**
 * REST Web Service
 *
 * @author felipe
 */
@Path("alumno")
public class AlumnoRest {

    @Context
    private UriInfo context;

    @Inject
    private AlumnoFacade customerFacade;
    
    public AlumnoRest() {
    }

    /**
     * Retrieves representation of an instance of modelo.rest.AlumnoRest
     * @return an instance of java.lang.String
     */
    @GET
    @Produces({"application/json"})
    public List<Alumno> getJson() {
        return customerFacade.findAll();
    }

    @GET
    @Path("/search/{name}")
    @Produces({"application/json"})
    public List<Alumno> findByName(@PathParam("name") String name) {
       return customerFacade.findByName(name);
    }

    /**
     * PUT method for updating or creating an instance of AlumnoRest
     * @param content representation for the resource
     */
    @PUT
    @Consumes(MediaType.APPLICATION_JSON)
    public void putJson(String content) {
    }
}
