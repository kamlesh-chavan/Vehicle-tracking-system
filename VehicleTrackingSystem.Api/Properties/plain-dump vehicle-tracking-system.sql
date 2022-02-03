--
-- PostgreSQL database dump
--

-- Dumped from database version 14.1
-- Dumped by pg_dump version 14.1

-- Started on 2022-02-04 01:34:38

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 3412 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 16536)
-- Name: devices; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.devices (
    device_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    device_number character varying(100) NOT NULL,
    name character varying(100) NOT NULL,
    description character varying(1000) NOT NULL,
    brand character varying(100) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_by character varying(100),
    created_date timestamp without time zone,
    modified_by character varying(100),
    modified_date timestamp without time zone
);


ALTER TABLE public.devices OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 16497)
-- Name: permissions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.permissions (
    permission_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    permission_name character varying(100),
    permission_description character varying(1000),
    is_active boolean DEFAULT false NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_by character varying(100),
    created_date timestamp without time zone,
    modified_by character varying(100),
    modified_date timestamp without time zone
);


ALTER TABLE public.permissions OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16455)
-- Name: roles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.roles (
    role_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    role_name character varying(100),
    description character varying(1000),
    is_active boolean DEFAULT false NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_by character varying(100),
    created_date timestamp without time zone,
    modified_by character varying(100),
    modified_date timestamp without time zone
);


ALTER TABLE public.roles OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 16445)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    first_name character varying(100) NOT NULL,
    last_name character varying(100) NOT NULL,
    email_id character varying(100) NOT NULL,
    password character varying(255) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_by character varying(100),
    created_date timestamp without time zone,
    modified_by character varying(100),
    modified_date timestamp without time zone
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16508)
-- Name: users_permission_mapper; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users_permission_mapper (
    users_permission_mapper_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    permission_id uuid NOT NULL,
    role_id uuid NOT NULL
);


ALTER TABLE public.users_permission_mapper OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 16471)
-- Name: users_roles_mapper; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users_roles_mapper (
    users_roles_mapper_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    user_id uuid NOT NULL,
    role_id uuid NOT NULL
);


ALTER TABLE public.users_roles_mapper OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16562)
-- Name: vehicle_device_mapper; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicle_device_mapper (
    vehicle_device_mapper_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    vehicle_id uuid NOT NULL,
    device_id uuid NOT NULL
);


ALTER TABLE public.vehicle_device_mapper OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16580)
-- Name: vehicle_location_mapper; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicle_location_mapper (
    vehicle_location_mapper_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    vehicle_device_mapper_id uuid NOT NULL,
    lat double precision NOT NULL,
    long double precision NOT NULL,
    "timestamp" timestamp without time zone NOT NULL,
    details jsonb,
    is_active boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.vehicle_location_mapper OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16526)
-- Name: vehicles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicles (
    vehicle_id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    maker character varying(100) NOT NULL,
    model character varying(100) NOT NULL,
    model_number character varying(100) NOT NULL,
    year character varying(4) NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    created_by character varying(100),
    created_date timestamp without time zone,
    modified_by character varying(100),
    modified_date timestamp without time zone
);


ALTER TABLE public.vehicles OWNER TO postgres;

--
-- TOC entry 3404 (class 0 OID 16536)
-- Dependencies: 216
-- Data for Name: devices; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.devices (device_id, device_number, name, description, brand, is_active, is_deleted, created_by, created_date, modified_by, modified_date) FROM stdin;
587dfa32-74a3-48d9-8312-6d7f00606af7	a1000	a-geo-track	geo-track device	geo-track	t	f	\N	\N	\N	\N
7aa28d59-aa8c-4e47-9b04-0a9a69aad102	b1000	b-geo-track	geo-track device	geo-track	t	f	\N	\N	\N	\N
ff41a834-0bb5-4f8f-b013-a526c63ae305	c1000	c-geo-track	geo-track device	geo-track	t	f	\N	\N	\N	\N
a4f8c514-bf1d-437c-b26a-f94d62fc0308	d1000	d-geo-track	geo-track device	geo-track	t	f	\N	\N	\N	\N
3bc0e241-528b-4d25-80ed-bf6483c16bb4	1a	1a Device	1a Device location tracking	A1 brand	t	f	\N	2022-02-03 23:46:16.810747	\N	\N
\.


--
-- TOC entry 3401 (class 0 OID 16497)
-- Dependencies: 213
-- Data for Name: permissions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.permissions (permission_id, permission_name, permission_description, is_active, is_deleted, created_by, created_date, modified_by, modified_date) FROM stdin;
b26633e4-1345-467a-8048-8b28aac34eab	GetLocation	GetLocation permission	t	f	\N	\N	\N	\N
\.


--
-- TOC entry 3399 (class 0 OID 16455)
-- Dependencies: 211
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.roles (role_id, role_name, description, is_active, is_deleted, created_by, created_date, modified_by, modified_date) FROM stdin;
6de507ff-3c62-4496-b6d3-9b7379ac78ca	Admin	Admin user role	t	f	\N	\N	\N	\N
77ba7094-9176-47d5-a5a4-89456385542c	User	Regular user role	t	f	\N	\N	\N	\N
\.


--
-- TOC entry 3398 (class 0 OID 16445)
-- Dependencies: 210
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (user_id, first_name, last_name, email_id, password, is_active, is_deleted, created_by, created_date, modified_by, modified_date) FROM stdin;
b56e924e-ddf8-43e9-a131-9e2e6fef39f3	Kamlesh	Chavan	demo@demo.com	password	t	f	\N	2022-01-29 12:16:29.017332	\N	\N
e32b55e0-6791-4f5d-aef9-bacbb6dc4c6d	Test	Test	test@test.com	password	t	f	\N	2022-01-30 11:54:11.054239	\N	\N
\.


--
-- TOC entry 3402 (class 0 OID 16508)
-- Dependencies: 214
-- Data for Name: users_permission_mapper; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users_permission_mapper (users_permission_mapper_id, permission_id, role_id) FROM stdin;
17f81ce5-c534-47b6-b956-3e5701dc5f73	b26633e4-1345-467a-8048-8b28aac34eab	6de507ff-3c62-4496-b6d3-9b7379ac78ca
\.


--
-- TOC entry 3400 (class 0 OID 16471)
-- Dependencies: 212
-- Data for Name: users_roles_mapper; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users_roles_mapper (users_roles_mapper_id, user_id, role_id) FROM stdin;
2d1a9943-ebd3-4cd3-a770-5f65f7ace499	b56e924e-ddf8-43e9-a131-9e2e6fef39f3	6de507ff-3c62-4496-b6d3-9b7379ac78ca
be22bac9-f238-4dcb-84ab-a6e681274a62	e32b55e0-6791-4f5d-aef9-bacbb6dc4c6d	77ba7094-9176-47d5-a5a4-89456385542c
\.


--
-- TOC entry 3405 (class 0 OID 16562)
-- Dependencies: 217
-- Data for Name: vehicle_device_mapper; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vehicle_device_mapper (vehicle_device_mapper_id, vehicle_id, device_id) FROM stdin;
8f75ed0b-9402-492d-86f6-2382a49d01ac	490dcef0-0816-48d8-84d1-d25f0c5ec038	587dfa32-74a3-48d9-8312-6d7f00606af7
e5063075-6f53-485a-9ec6-c951e0e1b5ec	884710e0-2f05-4928-8d5a-60a438020015	7aa28d59-aa8c-4e47-9b04-0a9a69aad102
d8bd8ba8-cd38-4144-9041-990f7dba2afd	ac1d7fd5-6fd6-489e-b179-67eacbda9ca0	ff41a834-0bb5-4f8f-b013-a526c63ae305
a89d4fdb-7b1e-4e12-a628-8ea873410596	b7433240-5443-4956-af94-3a5db0207bb3	a4f8c514-bf1d-437c-b26a-f94d62fc0308
c7ad91b3-ba3d-4b4a-bf6c-23689fec75bb	d05ef988-cfc9-4742-ba3f-d75d82543391	3bc0e241-528b-4d25-80ed-bf6483c16bb4
\.


--
-- TOC entry 3406 (class 0 OID 16580)
-- Dependencies: 218
-- Data for Name: vehicle_location_mapper; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vehicle_location_mapper (vehicle_location_mapper_id, vehicle_device_mapper_id, lat, long, "timestamp", details, is_active, is_deleted) FROM stdin;
b5568f4a-3d9e-4679-beb7-8e8a66e37c6b	8f75ed0b-9402-492d-86f6-2382a49d01ac	19.076	72.8777	2022-01-30 13:50:45.614513	\N	t	f
16b86ba9-82de-4d38-b6e3-47f33f44aba2	8f75ed0b-9402-492d-86f6-2382a49d01ac	35.6762	139.6503	2022-02-01 20:21:00.5319	{"Fuel": 50.0, "Speed": 100.0}	t	f
ae9ce289-693d-43b0-81be-1e1480e30f00	d8bd8ba8-cd38-4144-9041-990f7dba2afd	1.352	103.8198	2022-02-03 22:01:42.684098	{"Fuel": 0.0, "Speed": 0.0}	t	f
c1f7beec-c946-4297-98c0-ab9def402d5c	a89d4fdb-7b1e-4e12-a628-8ea873410596	12.8797	121.774	2022-02-03 22:19:30.86335	\N	t	f
e4145d4d-9190-400a-8b7e-35cd7d26a68a	c7ad91b3-ba3d-4b4a-bf6c-23689fec75bb	15.87	100.9925	2022-02-03 23:50:50.833831	{"Fuel": 100.0, "Speed": 10.0}	t	f
\.


--
-- TOC entry 3403 (class 0 OID 16526)
-- Dependencies: 215
-- Data for Name: vehicles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vehicles (vehicle_id, maker, model, model_number, year, is_active, is_deleted, created_by, created_date, modified_by, modified_date) FROM stdin;
490dcef0-0816-48d8-84d1-d25f0c5ec038	Hyundai	Ioniq	Ioniq2022	2022	t	f	\N	\N	\N	\N
884710e0-2f05-4928-8d5a-60a438020015	Lexus	Rx	Rx350	2022	t	f	\N	\N	\N	\N
ac1d7fd5-6fd6-489e-b179-67eacbda9ca0	Toyota	RAV4	RAV42022	2022	t	f	\N	\N	\N	\N
b7433240-5443-4956-af94-3a5db0207bb3	BMW	840i	840i2022	2022	t	f	\N	\N	\N	\N
5814158a-c653-4c51-9e82-92c64929bebc	Tesla	Model S3	S3	2021	t	f	\N	2022-01-30 15:14:18.845442	\N	\N
030afbf4-a63d-4bd0-9ef8-e952809e9c7b	Honda	Altos	Altos3	2022	t	f	\N	2022-02-02 00:34:45.96392	\N	\N
d05ef988-cfc9-4742-ba3f-d75d82543391	TATA	Nexon	Nexon NX	2022	t	f	\N	2022-02-03 22:40:16.446451	\N	\N
\.


--
-- TOC entry 3244 (class 2606 OID 16545)
-- Name: devices devices_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.devices
    ADD CONSTRAINT devices_pkey PRIMARY KEY (device_id);


--
-- TOC entry 3236 (class 2606 OID 16506)
-- Name: permissions permissions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.permissions
    ADD CONSTRAINT permissions_pkey PRIMARY KEY (permission_id);


--
-- TOC entry 3230 (class 2606 OID 16464)
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (role_id);


--
-- TOC entry 3240 (class 2606 OID 16513)
-- Name: users_permission_mapper users_permission_mapper_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_permission_mapper
    ADD CONSTRAINT users_permission_mapper_pkey PRIMARY KEY (users_permission_mapper_id);


--
-- TOC entry 3228 (class 2606 OID 16454)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- TOC entry 3234 (class 2606 OID 16476)
-- Name: users_roles_mapper users_roles_mapper_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_roles_mapper
    ADD CONSTRAINT users_roles_mapper_pkey PRIMARY KEY (users_roles_mapper_id);


--
-- TOC entry 3249 (class 2606 OID 16567)
-- Name: vehicle_device_mapper vehicle_device_mapper_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicle_device_mapper
    ADD CONSTRAINT vehicle_device_mapper_pkey PRIMARY KEY (vehicle_device_mapper_id);


--
-- TOC entry 3251 (class 2606 OID 16589)
-- Name: vehicle_location_mapper vehicle_location_mapper_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicle_location_mapper
    ADD CONSTRAINT vehicle_location_mapper_pkey PRIMARY KEY (vehicle_location_mapper_id);


--
-- TOC entry 3242 (class 2606 OID 16535)
-- Name: vehicles vehicles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_pkey PRIMARY KEY (vehicle_id);


--
-- TOC entry 3237 (class 1259 OID 16525)
-- Name: IX_users_permission_mapper_permission_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_users_permission_mapper_permission_id" ON public.users_permission_mapper USING btree (permission_id);


--
-- TOC entry 3238 (class 1259 OID 16524)
-- Name: IX_users_permission_mapper_role_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_users_permission_mapper_role_id" ON public.users_permission_mapper USING btree (role_id);


--
-- TOC entry 3231 (class 1259 OID 16487)
-- Name: IX_users_roles_mapper_role_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_users_roles_mapper_role_id" ON public.users_roles_mapper USING btree (role_id);


--
-- TOC entry 3232 (class 1259 OID 16488)
-- Name: IX_users_roles_mapper_user_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_users_roles_mapper_user_id" ON public.users_roles_mapper USING btree (user_id);


--
-- TOC entry 3245 (class 1259 OID 16579)
-- Name: IX_vehicle_device_mapper_permission_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_vehicle_device_mapper_permission_id" ON public.vehicle_device_mapper USING btree (vehicle_id);


--
-- TOC entry 3246 (class 1259 OID 16578)
-- Name: IX_vehicle_device_mapper_role_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_vehicle_device_mapper_role_id" ON public.vehicle_device_mapper USING btree (device_id);


--
-- TOC entry 3247 (class 1259 OID 16595)
-- Name: IX_vehicle_device_mapper_vehicle_device_mapper_vehicle_device_m; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_vehicle_device_mapper_vehicle_device_mapper_vehicle_device_m" ON public.vehicle_device_mapper USING btree (vehicle_device_mapper_id);


--
-- TOC entry 3255 (class 2606 OID 16519)
-- Name: users_permission_mapper fk_users_permission_mapper_roles_permission_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_permission_mapper
    ADD CONSTRAINT fk_users_permission_mapper_roles_permission_id FOREIGN KEY (permission_id) REFERENCES public.permissions(permission_id);


--
-- TOC entry 3254 (class 2606 OID 16514)
-- Name: users_permission_mapper fk_users_permission_mapper_roles_role_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_permission_mapper
    ADD CONSTRAINT fk_users_permission_mapper_roles_role_id FOREIGN KEY (role_id) REFERENCES public.roles(role_id);


--
-- TOC entry 3252 (class 2606 OID 16477)
-- Name: users_roles_mapper fk_users_roles_mapper_roles_role_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_roles_mapper
    ADD CONSTRAINT fk_users_roles_mapper_roles_role_id FOREIGN KEY (role_id) REFERENCES public.roles(role_id);


--
-- TOC entry 3253 (class 2606 OID 16482)
-- Name: users_roles_mapper fk_users_roles_mapper_users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users_roles_mapper
    ADD CONSTRAINT fk_users_roles_mapper_users_user_id FOREIGN KEY (user_id) REFERENCES public.users(user_id);


--
-- TOC entry 3257 (class 2606 OID 16573)
-- Name: vehicle_device_mapper fk_vehicle_device_mapper_devices_device_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicle_device_mapper
    ADD CONSTRAINT fk_vehicle_device_mapper_devices_device_id FOREIGN KEY (device_id) REFERENCES public.devices(device_id);


--
-- TOC entry 3256 (class 2606 OID 16568)
-- Name: vehicle_device_mapper fk_vehicle_device_mapper_vehicles_vehicle_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicle_device_mapper
    ADD CONSTRAINT fk_vehicle_device_mapper_vehicles_vehicle_id FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id);


--
-- TOC entry 3258 (class 2606 OID 16613)
-- Name: vehicle_location_mapper fk_vehicle_location_mapper_vehicle_device_mapper_vehicle_device; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicle_location_mapper
    ADD CONSTRAINT fk_vehicle_location_mapper_vehicle_device_mapper_vehicle_device FOREIGN KEY (vehicle_device_mapper_id) REFERENCES public.vehicle_device_mapper(vehicle_device_mapper_id) MATCH FULL;


-- Completed on 2022-02-04 01:34:39

--
-- PostgreSQL database dump complete
--

